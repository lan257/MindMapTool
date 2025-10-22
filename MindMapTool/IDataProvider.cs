using MindMapTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dm.net.buffer.ByteArrayBuffer;

namespace MindMapTool
{
    public interface IDataProvider
    {
        IRepository<MindMap> MindMaps { get; }
        IRepository<Core.Models.Node> Nodes { get; }
        IRepository<NodeRelation> NodeRelations { get; }
    }

}
