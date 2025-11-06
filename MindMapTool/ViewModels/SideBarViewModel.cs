using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MindMapTool.Tool;

namespace MindMapTool.ViewModels
{

    public partial class SideBarViewModel:ViewModelBase
    {
        public SideBarViewModel()
        {

        }

        [RelayCommand]
        private void DataEdit()
        {
            Log.info("打开数据接口编辑");
        }
        [RelayCommand]
        private void Settings() { 
            Log.info("打开设置功能模块");
        }   
    }
}
