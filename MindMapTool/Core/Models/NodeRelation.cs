using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Core.Models
{
    /// <summary>
    /// 节点关联
    /// </summary>
    public class NodeRelation
    {
        /// <summary>
        /// 节点关联Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 节点关联A(主动)
        /// </summary>
        public int NodeA {  get; set; }
        /// <summary>
        /// 节点关联B(被动)
        /// </summary>
        public int NodeB { get; set; }
    }
}
