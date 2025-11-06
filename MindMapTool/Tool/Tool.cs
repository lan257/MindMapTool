using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Tool
{
    public static class Tool
    {
        /// <summary>
        /// 列表转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToString<T>(List<T> list) where T : class
        {
            try
            {
                string result = "";
                foreach (T item in list)
                {
                    result += item.ToString() + "/n";
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
