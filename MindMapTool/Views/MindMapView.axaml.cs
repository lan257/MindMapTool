using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using MindMapTool.Core.Models;
using MindMapTool.Tool;
using MindMapTool.ViewModels;
using System.Linq;

namespace MindMapTool.Views;

public partial class MindMapView : UserControl
{

    public MindMapView()
    {
        InitializeComponent();
    }
    private void Render(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MindMapViewModel vm)
        {
            MindMapCanvas.Children.Clear();
            vm.FlatNodeList.Clear();
            var rootVm = vm.Render();
            foreach (var node in vm.FlatNodeList)
            {
                var border = new Border
                {
                    Background = Brushes.Beige,
                    CornerRadius = new CornerRadius(6),
                    Padding = new Thickness(6),
                    Child = new TextBlock { Text = node.Title }
                };

                Canvas.SetLeft(border, node.X);
                Canvas.SetTop(border, node.Y);
                MindMapCanvas.Children.Add(border);
            }

            DrawConnections(rootVm);
        }
    }

    public void DrawConnections(NodeViewModel parent)
    {
        foreach (var child in parent.Children)
        {
            var line = new Line
            {
                StartPoint = new Point(parent.X + 100, parent.Y + 20), // 假设节点宽约100
                EndPoint = new Point(child.X, child.Y + 20),
                Stroke = Brushes.Gray,
                StrokeThickness = 1
            };
            MindMapCanvas.Children.Add(line);
            DrawConnections(child);
            Log.info($"Drawn lines: {MindMapCanvas.Children.Count}");
        }
    }
}