using CommunityToolkit.Mvvm.ComponentModel;
using MindMapTool.Core.Models;
using System.Collections.ObjectModel;

namespace MindMapTool.ViewModels
{
    public partial class NodeViewModel : ViewModelBase
    {
        [ObservableProperty]private int _id;
        [ObservableProperty] private string? _title;
        [ObservableProperty] private ObservableCollection<NodeViewModel>? _children;
        [ObservableProperty] private double _x, _y;
        public NodeViewModel(Node node,double x,double y)
        {
            Id = node.Id;
            Title = node.Title;
            Children = [];
            X = x;
            Y = y;
        }
    }
}
