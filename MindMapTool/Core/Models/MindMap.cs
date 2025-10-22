using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Core.Models
{
    /// <summary>
    /// 思维导图
    /// </summary>
    public class MindMap
    {
        /// <summary>
        /// 思维导图Id
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 思维导图标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 思维导图内容（简介）
        /// </summary>
        public string? Content { get; set; }
    }
}
