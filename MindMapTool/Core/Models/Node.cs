using SqlSugar;
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
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 父节点Id,当为0表示为根节点
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
        /// <summary>
        /// 子节点集合，SQLSugar忽视此属性
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Node> Children { get; set; } = [];
        /// <summary>
        /// 节点信息转换为字符串
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return $"Id: {Id}, ParentId: {ParentId}, MapId: {MapId}, Title: {Title}, Content: {Content}";
        }
        /// <summary>
        /// 节点列表转换为字符串
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static string ToString(List<Node> list)
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
