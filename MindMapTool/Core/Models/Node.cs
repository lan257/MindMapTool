using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Core.Models
{
    /// <summary>
    /// 节点
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 思维导图Id
        /// </summary>
        public int MapId { get; set; }
        /// <summary>
        /// 节点标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 节点内容
        /// </summary>
        public string? Content { get; set; }
    }
}
