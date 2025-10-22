using Microsoft.Data.Sqlite;
using MindMapTool.Sqlite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MindMapTool.Core.Models
{
    public class Map
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public Map(int Id,string Name)
        {
            this.Id= Id;
            this.Name= Name;
            this.CreatedAt = DateTime.Now;
        }
        public Map() { }
    }
}
