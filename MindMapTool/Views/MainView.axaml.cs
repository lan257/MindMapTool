using Avalonia.Controls;
using Avalonia.Interactivity;
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
        Debug.WriteLine("Add!");
    }
    private void SelectMap(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Select!");
    }
}