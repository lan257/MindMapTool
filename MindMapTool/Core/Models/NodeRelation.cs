using SqlSugar;
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
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 节点关联A(主动)
        /// </summary>
        public int NodeA {  get; set; }
        /// <summary>
        /// 节点关联B(被动)
        /// </summary>
        public int NodeB { get; set; }
        /// <summary>
        /// 节点A的思维导图ID
        /// </summary>
        public int MA { get; set; }
        /// <summary>
        /// 节点B的思维导图ID
        /// </summary>
        public int MB { get; set; }
        /// <summary>
        /// 节点关联转化为字符串
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return $"Id:{Id},NodeA:{NodeA},NodeB:{NodeB},MA:{MA},MB:{MB}";
        }
        /// <summary>
        /// 节点关联转换为字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToString(List<NodeRelation> list)
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
