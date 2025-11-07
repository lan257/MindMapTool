using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Sqlite
{
    using MindMapTool.Core.Models;
    using MindMapTool.Tool;
    using SqlSugar;

    public class LocalRepository<T> : IRepository<T> where T : class, new()
    {
        public readonly ISqlSugarClient _db;

        public LocalRepository(ISqlSugarClient db)
        {
            _db = db;
            _db.CodeFirst.InitTables<T>(); // 自动建表
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync() =>
            await _db.Queryable<T>().ToListAsync();
        /// <summary>
        /// 获取指定数据（通过主键）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<T?> GetAsync(int id) =>
            await _db.Queryable<T>().InSingleAsync(id);
        /// <summary>
        /// 新增数据（全量）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity) =>
            await _db.Insertable(entity).ExecuteReturnIdentityAsync();
        /// <summary>
        /// 更新数据（全量）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity) =>
            await _db.Updateable(entity).ExecuteCommandAsync();
        /// <summary>
        /// 删除数据（通过主键）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id) =>
            await _db.Deleteable<T>().In(id).ExecuteCommandAsync();
    }
    public class INodeRepository : LocalRepository<Node>
    {
        public INodeRepository(ISqlSugarClient db) : base(db) { }
        //节点表复杂删除
        public async Task DeleteAsync(int id)
        {
            Log.info($"删除节点及子节点，并清理关系，root = {id}");

            // 1. 获取当前节点所属导图Id（用于 relation 优化过滤）
            var mapId = await _db.Queryable<Node>()
                                 .Where(n => n.Id == id)
                                 .Select(n => n.MapId)
                                 .SingleAsync();

            // 2. 获取该节点 + 子树所有节点ID
            var ids = await GetSubtreeIdsAsync(id);

            if (ids.Count == 0)
                return;

            try
            {
                _db.Ado.BeginTran();

                // 3. 删除关联（NodeA/NodeB 或 MA/MB 均可用）
                await _db.Deleteable<NodeRelation>()
                         .Where(r => ids.Contains(r.NodeA) || ids.Contains(r.NodeB))
                         .ExecuteCommandAsync();

                // 若使用 MA/MB 字段，可以写得更快（推荐）：
                // await _db.Deleteable<NodeRelation>()
                //          .Where(r => r.MA == mapId && ids.Contains(r.NodeA))
                //          .Or(r => r.MB == mapId && ids.Contains(r.NodeB))
                //          .ExecuteCommandAsync();

                // 4. 删除节点
                await _db.Deleteable<Node>()
                         .In(ids)
                         .ExecuteCommandAsync();

                _db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _db.Ado.RollbackTran();
                Log.error("删除节点失败："+ ex);
                throw;
            }
        }
        /// <summary>
        /// 获取某节点的所有子节点（包含自己）
        /// </summary>
        private async Task<List<int>> GetSubtreeIdsAsync(int rootId)
        {
            // 先一次性取出所有节点（单机版无性能压力）
            var allNodes = await _db.Queryable<Node>()
                                    .Select(n => new { n.Id, n.ParentId })
                                    .ToListAsync();

            var dict = allNodes.GroupBy(n => n.ParentId)
                               .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

            var result = new List<int>();
            var stack = new Stack<int>();
            stack.Push(rootId);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                result.Add(current);

                if (dict.TryGetValue(current, out var children))
                {
                    foreach (var child in children)
                        stack.Push(child);
                }
            }

            return result;
        }
        /// <summary>
        /// 获取思维导图所有节点
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public async Task<List<Node>> GetAllAsync(int mapId) =>
            await _db.Queryable<Node>()
                     .Where(n => n.MapId == mapId)
                     .ToListAsync();

    }
    public class IMindMapRepository : LocalRepository<MindMap>
    {
        public IMindMapRepository(ISqlSugarClient db) : base(db){}
        //思维导图表复杂删除
        public async Task DeleteAsync(int id)
        {
            Log.info("删除思维导图表-复杂");
            try
            {
                //删除节点表
                await _db.Deleteable<Node>().Where(x => x.MapId == id).ExecuteCommandAsync();
                //删除节点关联表
                await _db.Deleteable<NodeRelation>().Where(x => x.MA == id || x.MB == id).ExecuteCommandAsync();
                //删除思维导图表
                await _db.Deleteable<MindMap>().Where(x => x.Id == id).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                Log.error("删除思维导图表-复杂失败："+ ex);
                throw;
            }
        }
    }

}

