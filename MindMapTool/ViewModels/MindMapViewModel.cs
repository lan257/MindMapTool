using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MindMapTool.Core.Models;
using MindMapTool.Tool;
using MindMapTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.ViewModels
{
    public partial class MindMapViewModel : ViewModelBase
    {
        [ObservableProperty] private int _id;
        [ObservableProperty] private ObservableCollection<NodeViewModel> _flatNodeList = [];
        public MindMapViewModel()
        {
            Id = _id;
            FlatNodeList = _flatNodeList;
        }

        /// <summary>
        /// 渲染思维导图
        /// </summary>
        public NodeViewModel Render()
        {
            var rootNode = GetNodeTree();
            var rootVM = BuildViewModelTree(rootNode, 250,320); // 根节点初始坐标
            FlatNodeList.Clear();
            FlattenTree(rootVM, FlatNodeList);
            return rootVM;
        }

        private void FlattenTree(NodeViewModel node, ObservableCollection<NodeViewModel> list)
        {
            list.Add(node);
            foreach (var child in node.Children)
                FlattenTree(child, list);
        }
        /// <summary>
        /// 获取思维导图节点树形结构
        /// </summary>
        /// <returns></returns>
        public Node GetNodeTree()
        {
            var nodes = DataProviderFactory.Current.Nodes.GetAllAsync(Id).Result;
            var lookup = nodes.ToDictionary(n => n.Id);
            var root = new Node();
            foreach (var n in nodes)
            {
                if (n.ParentId == 0)
                {
                    root = n;
                }
                else if (lookup.TryGetValue(n.ParentId, out var parent))
                {
                    parent.Children.Add(n);
                }
            }
            return root ?? throw new Exception("未找到根节点");
        }
        /// <summary>
        /// 转换为 NodeViewModel 树
        /// </summary>
        /// <param name="node"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public NodeViewModel BuildViewModelTree(Node node, double x, double y)
        {
            var vm = new NodeViewModel(node, x, y);

            if (node.Children.Count == 0)
                return vm;

            double childX = x + 200; // 子节点向右偏移
            double spacingY = 80;    // 垂直间距

            // 计算整棵子树的高度
            double totalHeight = node.Children.Count * spacingY;
            double startY = y - (totalHeight - spacingY) / 2;

            foreach (var child in node.Children)
            {
                var childVm = BuildViewModelTree(child, childX, startY);
                vm.Children.Add(childVm);
                startY += spacingY;
            }

            return vm;
        }

    }
}
