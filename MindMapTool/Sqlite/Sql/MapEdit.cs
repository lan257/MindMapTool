using MindMapTool.Core.Models;
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
        void init()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbPath = Path.Combine(appData, "MindMapTool", "MindMap.db");
            Setting.db.CodeFirst.InitTables<Map>();
        }
        void addMap(Map map)
        {
            var newMap = new Map { Name = "我的第一张思维导图" };
            Setting.db.Insertable(newMap).ExecuteCommand();
        }
        void deleteMap(Map map)
        {
            Setting.db.Deleteable<Map>().Where(m => m.Id == map.Id).ExecuteCommand();
        }
        void updateMap(Map map)
        {
            Setting.db.Updateable(map).ExecuteCommand();
        }
        List<Map> getAllMaps()
        {
            return Setting.db.Queryable<Map>().ToList();
        }
    }
}
