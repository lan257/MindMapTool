using SqlSugar;
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
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 思维导图标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 思维导图内容（简介）
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// 思维导图信息转化为字符串
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Content: {Content}";
        }
        /// <summary>
        /// 思维导图信息转换为字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToString(List<MindMap> list)
        {
            string result = "";
            foreach (var item in list)
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
    }
}
