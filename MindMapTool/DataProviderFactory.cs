using MindMapTool.Core.Models;
using MindMapTool.Sqlite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dm.net.buffer.ByteArrayBuffer;

namespace MindMapTool
{
    public static class DataProviderFactory
    {
        private static IDataProvider? _current;

        public static void Initialize()
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = "DataSource=MindMap.db",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            // 创建数据库和表结构
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables<MindMap, Core.Models.Node, NodeRelation>();

            _current = new LocalDataProvider(db);
        }

        public static IDataProvider Current =>
            _current ?? throw new InvalidOperationException("DataProvider 尚未初始化");
    }
}
