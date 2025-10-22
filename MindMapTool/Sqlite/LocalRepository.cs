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

        public async Task<List<T>> GetAllAsync() =>
            await _db.Queryable<T>().ToListAsync();

        public async Task<T?> GetAsync(int id) =>
            await _db.Queryable<T>().InSingleAsync(id);

        public async Task<int> AddAsync(T entity) =>
            await _db.Insertable(entity).ExecuteReturnIdentityAsync();

        public async Task UpdateAsync(T entity) =>
            await _db.Updateable(entity).ExecuteCommandAsync();

        public async Task DeleteAsync(int id) =>
            await _db.Deleteable<T>().In(id).ExecuteCommandAsync();
    }
}

