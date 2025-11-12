using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MindMapTool.Core.Models;
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
            Log.info("渲染思维导图");
        }
    }
}
