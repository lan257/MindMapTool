using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Sqlite
{
    using SqlSugar;

    public class LocalRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly ISqlSugarClient _db;

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
}

