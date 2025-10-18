using Avalonia.Controls;
using Avalonia.Interactivity;
using MindMapTool.Core.Models;
using MindMapTool.Sqlite.Sql;
using MindMapTool.Tool;
using System.Collections.Generic;
using System.Diagnostics;

namespace MindMapTool.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    private void AddMap(object? sender, RoutedEventArgs e)
    {
        Map map = new Map(1, "New Map");
        MapEdit.AddMap(map);
        Log.info("Add!"+map.Name);
    }
    private void SelectMap(object? sender, RoutedEventArgs e)
    {
        List<Map> selectedMap = MapEdit.GetAllMaps();
        Log.error("Select!" + selectedMap[0].Name);

    }
}