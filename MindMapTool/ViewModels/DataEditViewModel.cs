using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MindMapTool.Core.Models;
using MindMapTool.Sqlite;
using MindMapTool.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMapTool.ViewModels
{
    public partial class DataEditViewModel: ViewModelBase
    {
        /// <summary>
        /// 数据编辑视图模型id,思维导图Id,思维导图父节点Id,数据类型索引
        /// </summary>
        [ObservableProperty] private int _iD,_iD2,_iD3, _dataTypeIndex;
        /// <summary>
        /// 数据编辑视图模型标题、内容,数据编辑查询结果
        /// </summary>
        [ObservableProperty] private string _title,_content,_data;

        //数据编辑模型
        private Object GetDataModel()
            {
                if (DataTypeIndex == 0)
                {
                    return new MindMap()
                    {
                        Id = ID,
                        Title = Title,
                        Content = Content
                    };
                }
                else if (DataTypeIndex == 1)
                {
                    return new Node()
                    {
                        Id = ID,
                        ParentId = ID3,
                        MapId = ID3==0?ID2:dp.Nodes.GetAsync(ID3).Result.MapId,
                        Title = Title,
                        Content = Content
                    };
                }
                else if (DataTypeIndex == 2)
                {
                    return new NodeRelation()
                    {
                        Id = ID,
                        NodeA = int.Parse(Title),
                        NodeB = int.Parse(Content)
                    };
                }
                else
                {
                    return new MindMap()
                    {
                        Id = ID,
                        Title = Title,
                        Content = Content
                    };
                }
            }

        IDataProvider dp = DataProviderFactory.Current;
        //数据编辑查询结果
        public DataEditViewModel()
        {
            ID = _iD;
            ID2 = _iD2;
            ID3 = _iD3;
            Title = _title;
            Content = _content;
            Data = "";
            DataTypeIndex = 0;
        }
        //添加数据到数据库
        [RelayCommand]
        public  async void Add()
        {
            if (DataTypeIndex == 0)
            {
                MindMap mindMap = (MindMap)GetDataModel();
                //TODO:添加思维导图数据到数据库
                await dp.MindMaps.AddAsync(mindMap);
                Data += "Add MindMap：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Add MindMap：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 1)
            {
                //TODO:添加节点数据到数据库
                Node node = (Node)GetDataModel();
                await dp.Nodes.AddAsync(node);
                Data += "Add Node：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Add Node：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 2)
            {
                //TODO:添加节点关系数据到数据库
                NodeRelation nodeRelation = (NodeRelation)GetDataModel();
                nodeRelation.MA = dp.Nodes.GetAsync(nodeRelation.NodeA).Result.MapId;
                nodeRelation.MB = dp.Nodes.GetAsync(nodeRelation.NodeB).Result.MapId;
                await dp.NodeRelations.AddAsync(nodeRelation);
                Data += "Add NodeRelation：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Add NodeRelation：" + ID + " " + Title + " " + Content);
            }
            else
            {
                //TODO:添加其他数据到数据库
                Log.info("Add Other：异常" + ID + " " + Title + " " + Content);
            }
        }
        //更新数据到数据库
        [RelayCommand]
        public void Update()
        {
            //TODO:更新数据到数据库
            if (DataTypeIndex == 0)
            {
                MindMap DataModel = (MindMap)GetDataModel();
                dp.MindMaps.UpdateAsync(DataModel);
                Data += "Update MindMap：" + ID + " " + Title + " " + Content + "\n";
                //TODO:更新思维导图数据到数据库
                Log.info("Update MindMap：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 1)
            {
                //TODO:更新节点数据到数据库
                Node node = (Node)GetDataModel();
                dp.Nodes.UpdateAsync(node);
                Data += "Update Node：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Update Node：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 2)
            {
                //TODO:更新节点关系数据到数据库
                NodeRelation nodeRelation = (NodeRelation)GetDataModel();
                nodeRelation.MA = dp.Nodes.GetAsync(nodeRelation.NodeA).Result.MapId;
                nodeRelation.MB = dp.Nodes.GetAsync(nodeRelation.NodeB).Result.MapId;
                dp.NodeRelations.UpdateAsync(nodeRelation);
                Data += "Update NodeRelation：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Update NodeRelation：" + ID + " " + Title + " " + Content);
            }
            else
            {
                //TODO:更新其他数据到数据库
                Log.info("Update Other：" + ID + " " + Title + " " + Content);
            }
        }
        //删除数据到数据库
        [RelayCommand]
        public void Delete()
        {
            //TODO:删除数据到数据库
            if (DataTypeIndex == 0)
            {
                //TODO:删除思维导图数据到数据库
                dp.MindMaps.DeleteAsync(ID);
                Data += "Delete MindMap：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Delete MindMap：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 1)
            {
                //TODO:删除节点数据到数据库
                dp.Nodes.DeleteAsync(ID);
                Data += "Delete Node：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Delete Node：" + ID + " " + Title + " " + Content);
            }
            else if (DataTypeIndex == 2)
            {
                //TODO:删除节点关系数据到数据库
                dp.NodeRelations.DeleteAsync(ID);
                Data += "Delete NodeRelation：" + ID + " " + Title + " " + Content + "\n";
                Log.info("Delete NodeRelation：" + ID + " " + Title + " " + Content);
            }
            else
            {
                //TODO:删除其他数据到数据库
                Log.info("Delete Other：" + ID + " " + Title + " " + Content);
            }
            Log.info("Delete：" + DataTypeIndex);
        }

        //全量查询数据
        [RelayCommand]
        public void Query()
        {
            //TODO:查询数据
            if (DataTypeIndex == 0)
            {
                //TODO:查询思维导图数据
                List<MindMap> data = dp.MindMaps.GetAllAsync().Result;
                Data += "查询成功\n" + MindMap.ToString(data)+"\n";
                Log.info("Query MindMap：" + Data);
            }
            else if (DataTypeIndex == 1)
            {
                //TODO:查询节点数据
                List<Node> data = dp.Nodes.GetAllAsync().Result;
                Data += "查询成功\n" + Node.ToString(data) + "\n";
                Log.info("Query Node：" + Data);
            }
            else if (DataTypeIndex == 2)
            {
                //TODO:查询节点关系数据
                List<NodeRelation> data = dp.NodeRelations.GetAllAsync().Result;
                Data += "查询成功\n" + NodeRelation.ToString(data) + "\n";
                Log.info("Query NodeRelation：" + Data);
            }
            else
            {
                //TODO:查询其他数据
                Data += "暂无数据"+"\n";
            }
            Log.info("Query：" + DataTypeIndex);
        }
        [RelayCommand]
        public void Clear()
        {
            Data = "";
        }
    }
}
