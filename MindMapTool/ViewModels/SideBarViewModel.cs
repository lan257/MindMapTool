using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCore.Base.App;
using Microsoft.Extensions.DependencyInjection;
using MindMapTool.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.ViewModels
{

    public partial class SideBarViewModel:ViewModelBase
    {
        public SideBarViewModel()
        {

        }

        [RelayCommand]
        // 打开数据接口编辑页面
        private void DataEdit()
        {
            App.Services.GetRequiredService<MainViewModel>().ViewChanged(0);
            Log.info("打开数据接口编辑");
        }
        [RelayCommand]
        // 打开思维导图查看页面
        private void MindMap() {
            App.Services.GetRequiredService<MainViewModel>().ViewChanged(1);
            Log.info("打开思维导图查看");
        }
        [RelayCommand]
        // 打开设置功能模块
        private void Settings() { 
            Log.info("打开设置功能模块");
        }   
    }
}
