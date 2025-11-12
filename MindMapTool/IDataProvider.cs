using MindMapTool.Core.Models;
using MindMapTool.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dm.net.buffer.ByteArrayBuffer;

namespace MindMapTool
{
    /// <summary>
    /// Interface for data provider.
    /// </summary>
    public interface IDataProvider
    {
        IMindMapRepository MindMaps { get; }
        INodeRepository Nodes { get; }
        IRepository<NodeRelation> NodeRelations { get; }
    }

}
