using Microsoft.Data.Sqlite;
using MindMapTool.Core.Models;
using MindMapTool.Sqlite;
using SqlSugar;
using System;
using System.IO;

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

var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
var dbPath = Path.Combine(appData, "MindMapTool", "MindMap.db");
Setting.db.CodeFirst.InitTables<Map>();
