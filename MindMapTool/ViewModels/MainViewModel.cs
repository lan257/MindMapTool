using CommunityToolkit.Mvvm.ComponentModel;
namespace MindMapTool.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
    /// <summary>
    /// 是否显示思维导图节点可视化展示,默认显示数据编辑
    /// </summary>
    [ObservableProperty] private bool _mindMapSelected, _dataEditSelected;

    public MainViewModel()
    {
        MindMapSelected = true;
        DataEditSelected = false;
    }
    /// <summary>
    /// 功能视图切换
    /// 0:DataEdit、数据编辑
    /// 1:MindMap、思维导图节点可视化展示
    /// </summary>
    /// <param name="view"></param>
    public void ViewChanged(int view)
    {
        MindMapSelected = false;
        DataEditSelected = false;
        //数据编辑
        if (view == 0)
        {
            DataEditSelected = true;
        }
        //节点展示
        else if (view == 1)
        {
            MindMapSelected = true;
        }
        
    }
}
