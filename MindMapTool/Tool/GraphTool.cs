using GraphX.PCL.Common.Models;
using MindMapTool.ViewModels;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.Tool
{
    public class GraphTool
    {
        public static void ApplySimpleTreeLayout(List<NodeViewModel> nodeVMs)
        {
            double x = 0;
            double y = 0;
            double levelGap = 250;
            double nodeGap = 100;

            var groups = nodeVMs.GroupBy(n => n.Data.ParentId);
            var roots = nodeVMs.Where(n => n.Data.ParentId == 0).ToList();

            foreach (var root in roots)
            {
                LayoutRecursive(root, nodeVMs, x, ref y, levelGap, nodeGap);
                y += 200;
            }
        }

        private static void LayoutRecursive(NodeViewModel node, List<NodeViewModel> all, double x, ref double y, double levelGap, double nodeGap)
        {
            node.X = x;
            node.Y = y;

            var children = all.Where(n => n.Data.ParentId == node.Data.Id).ToList();
            double childY = y;

            foreach (var c in children)
            {
                LayoutRecursive(c, all, x + levelGap, ref childY, levelGap, nodeGap);
                childY += nodeGap;
            }
        }
    }
    public class GraphNode : VertexBase
    {
        public NodeViewModel VM { get; }

        public GraphNode(NodeViewModel vm)
        {
            VM = vm;
        }

        public override string ToString() => VM.Title;
    }
    public class GraphEdge : EdgeBase<GraphNode>
    {
        public GraphEdge(GraphNode source, GraphNode target) : base(source, target) { }
    }
}
