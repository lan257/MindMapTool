using MindMapTool.Core.Models;
using MindMapTool.Sqlite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dm.net.buffer.ByteArrayBuffer;
using Node = MindMapTool.Core.Models.Node;

namespace MindMapTool
{
    public class LocalDataProvider : IDataProvider
    {
        public LocalDataProvider(ISqlSugarClient db)
        {
            MindMaps = new LocalRepository<MindMap>(db);
            Nodes = new LocalRepository<Node>(db);
            NodeRelations = new LocalRepository<NodeRelation>(db);
        }

        public IRepository<MindMap> MindMaps { get; }
        public IRepository<Node> Nodes { get; }
        public IRepository<NodeRelation> NodeRelations { get; }
    }
}
