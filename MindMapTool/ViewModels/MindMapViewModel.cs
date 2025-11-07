using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MindMapTool.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.ViewModels
{
    public partial class MindMapViewModel : ViewModelBase
    {
        [ObservableProperty] private int _iD;
        public ObservableCollection<NodeViewModel> Nodes { get; } = new();
        public MindMapViewModel()
        {
            ID = _iD;
        }

        [RelayCommand]
        /// <summary>
        /// 渲染思维导图
        /// </summary>
        public void Render()
        {
            var rawNodes = DataProviderFactory.Current.Nodes.GetAllAsync(ID).Result;

            var vmList = rawNodes.Select(n => new NodeViewModel(n)).ToList();

            GraphTool.ApplySimpleTreeLayout(vmList);

            Nodes.Clear();
            foreach (var vm in vmList)
                Nodes.Add(vm);
        }
    }
}
