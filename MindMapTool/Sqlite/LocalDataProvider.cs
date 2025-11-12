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
    /// <summary>
    /// Local data provider implementation.
    /// </summary>
    public class LocalDataProvider : IDataProvider
    {
        public LocalDataProvider(ISqlSugarClient db)
        {
            MindMaps = new IMindMapRepository(db);
            Nodes = new INodeRepository(db);
            NodeRelations = new LocalRepository<NodeRelation>(db);
        }

        public IMindMapRepository MindMaps { get; }
        public INodeRepository Nodes { get; }
        public IRepository<NodeRelation> NodeRelations { get; }
    }
}
