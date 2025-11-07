using CommunityToolkit.Mvvm.ComponentModel;
using GraphX.Controls;
using MindMapTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.ViewModels
{
    public class NodeViewModel : ObservableObject
    {
        public Node Data { get; }

        public double X { get; set; }
        public double Y { get; set; }

        public string Title => Data.Title;
        public string Summary => Data.Content;

        public NodeViewModel(Node data) => Data = data;
    }


}
