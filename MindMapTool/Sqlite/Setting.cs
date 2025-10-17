using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Sqlite
{
    public class Setting
    {
        public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig
        {
            ConnectionString = "Data Source=MindMap.db",
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true
        });
    }
}
