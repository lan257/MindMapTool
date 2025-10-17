using Microsoft.Data.Sqlite;
using MindMapTool.Core.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Sqlite.Sql
{
    public class MapEdit
    {
        public static void Init()
        {
            var connectionString = "Data Source=MindMap.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            // 可以创建表
            var command = connection.CreateCommand();
            command.CommandText =
            @"
CREATE TABLE IF NOT EXISTS Maps (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);
";
            command.ExecuteNonQuery();

            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = "Data Source=MindMap.db",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            });
            Setting.db.CodeFirst.InitTables<Map>();
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbPath = Path.Combine(appData, "MindMapTool", "MindMap.db");
        }
        void AddMap(Map map)
        {
            Setting.db.Insertable(map).ExecuteCommand();
        }
        void DeleteMap(Map map)
        {
            Setting.db.Deleteable<Map>().Where(m => m.Id == map.Id).ExecuteCommand();
        }
        void UpdateMap(Map map)
        {
            Setting.db.Updateable(map).ExecuteCommand();
        }
        List<Map> GetAllMaps()
        {
            return Setting.db.Queryable<Map>().ToList();
        }
    }
}
