```markdown
# MindMapTool — Project Structure (Single-machine Avalonia)

概述
- 目标：单机轻量版 Avalonia 应用，MVP 可运行，后续可平滑迁移到 WebAPI/后端服务。
- 技术栈：Avalonia, CommunityToolkit.Mvvm, Semi / Ursa, SQLSugar + SQLite, WebView + Toast UI Editor

目录结构（顶层）
- /MindMapTool
  - /ViewModels                       # Avalonia UI 项目ViewModel层
  - /Views                            # Avalonia UI 项目View层
  - /Core                             # 核心领域模型与接口（跨平台可复用）
  - /Infrastructure.Sqlite            # SQLite + SQLSugar 实现（StorageProvider）
  - /MindMapTool.Services             # 应用服务（Navigation, Search, Indexing）
  - /MindMapTool.UI.Controls          # 自定义控件（MapCanvas, NodeView）
  - /MindMapTool.Plugins              # 插件接口 & 本地插件示例
  - /MindMapTool.WebEditor            # 本地静态文件 for Toast UI Editor (html/js/css)
- /migrations                          # SQL schema, migration scripts
- /scripts                             # build / package / helper scripts
- /docs                                # 设计文档、API 说明、安装指南
- /tests                               # 单元测试与集成测试
- /assets                              # 图片、图标、主题、字体
- /build                               # CI 构建脚本 (github actions/workflows)

每个项目简要职责与关键类型
1. MindMapTool.Core
   - 责任：定义领域模型、DTO、仓储接口、服务接口和常量，独立于实现。
   - 关键文件：
     - Models/Map.cs, Models/Node.cs, Models/KnowledgeCard.cs, Models/Link.cs
     - Interfaces/IStorageProvider.cs
     - Interfaces/IGraphProvider.cs
     - Interfaces/ISearchProvider.cs
     - ViewModels/BaseViewModel.cs (依赖 CommunityToolkit.Mvvm 的基类)

2. MindMapTool.Infrastructure.Sqlite
   - 责任：实现 IStorageProvider、IGraphProvider 与数据迁移，SQLSugar 配置与 helper。
   - 要点：
     - 提供 SqlSugarClientFactory，根据配置建立 SQLite 连接（文件路径、journal_mode）
     - 提供迁移脚本管理（migrations/*.sql）
     - 支持数据导入/导出 JSON Bundle
   - 关键文件：
     - SqliteStorageProvider.cs
     - SqlSugarFactory.cs
     - Migrations/*.sql

3. MindMapTool.Services
   - 责任：实现导航（NavigationService）、全局跳转（QuickJumpService）、索引/搜索（IndexingService，Lucene.NET 可选）、Markdown 管理（MarkdownService）
   - 要点：
     - NavigationService.OpenMap(mapId), SelectNode(nodeId), EnsureMapLoaded(mapId)
     - IndexingService 抽象出同步索引/查询接口（便于后端接入）

4. MindMapTool.App (Avalonia)
   - 责任：界面、视图绑定、启动、主题/语言切换、命令和 UI 流程。
   - 结构：
     - App.axaml / App.axaml.cs
     - Views/ (MainWindow, MapEditorView, MapListView, KnowledgeCardView, QuickJumpOverlay)
     - ViewModels/ (MainWindowVM, MapEditorVM, NodeVM, KnowledgeCardVM, QuickJumpVM)
     - Controls/ (MapCanvas 控件, NodeControl)
   - 关键点：
     - 使用 CommunityToolkit.Mvvm (ObservableObject, RelayCommand, IAsyncRelayCommand)
     - 使用 DI 注入服务（ServiceCollection）
     - 使用 Resources/Styles 引入 Semi / Ursa 主题

5. MindMapTool.UI.Controls
   - 责任：MapCanvas（渲染节点/边）、节点虚拟化、交互事件（拖放/缩放/框选）
   - 要点：
     - 使用 Avalonia 的绘制 API 或 SkiaSharp 做高性能渲染（复杂场景下）
     - 提供定位 API（ScrollToNode(nodeId), HighlightNode(nodeId)）

6. MindMapTool.Plugins
   - 责任：定义 IPlugin 接口并加载 plugins/ 文件夹下的 DLL
   - 扩展点：StorageProvider 替换、导入导出、布局算法、导出报告

7. MindMapTool.WebEditor
   - 责任：放置 Toast UI Editor 的静态文件 (editor.html, editor.js, css)，通过本地 WebView 加载并与 .NET 交互
   - 要点：
     - 使用 Avalonia.WebView (注意不同平台的 runtime 支持)
     - JS <-> .NET Bridge: 编辑器内容变化通过 bridge 通知保存（postMessage 或自定义 bridge）

配置与 DI
- 使用 Microsoft.Extensions.DependencyInjection 在启动时注册：
  - Core 服务、ViewModels、SqliteStorageProvider（IStorageProvider）、NavigationService、IndexingService
- 在 App 启动或 Bootstrapper 中完成 ServiceProvider 构建，并把 ServiceProvider 注入到 ViewModelsLocator 或通过构造函数注入

持久化配置
- 应用配置文件（appsettings.json 或 Settings 存储表）
  - db.filePath => 默认 "%AppData%/MindMapTool/data.sqlite"（可在 UI 中修改并迁移）
  - theme, language, autosave_interval, backup_path

Markdown 编辑器集成（WebView + Toast UI）
- 将 editor.html 放在项目的嵌入资源或 /MindMapTool.WebEditor 输出目录
- 在 KnowledgeCardView 中放置 WebView 控件，加载本地 editor.html
- 使用 postMessage 或 WebView 的 JS bridge 在 JS -> C# 之间传递 save/load/getPreview 请求
- 注意跨平台：Windows 使用 WebView2; Linux/macOS 需要合适的 WebView 后端（Avalonia.WebView 支持多后端），若不可用则回退到 AvaloniaEdit + Markdig

数据库与 schema
- 使用 guid (string) 作为主键
- schema 放在 /migrations/schema.sql（你之前有的 schema 可直接放置）
- SQLSugar 配置：
  - IsAutoCloseConnection = true
  - Aop.OnLogExecuted 用于调试 SQL
  - 事务封装、批量操作

测试与 CI
- tests/ 包含：
  - Unit: Core 模型验证、StorageProvider 单元测试（利用临时 SQLite 文件）
  - Integration: 以真实 SQLite 文件做 CRUD 测试
- CI:
  - GitHub Actions：build on push, run tests, create artifacts（Windows/Linux/macOS 跨平台打包可选）

打包与发布
- 单机版可输出单文件（dotnet publish -r win-x64 / linux-x64 / osx-x64）
- 附带 assets（webeditor 静态文件、默认模板）
- 提供 installer 或 zip 包

开发与调试建议
- 首先实现 Core 接口与 SqliteStorageProvider 的基本 CRUD，保证数据层稳定
- 同步开发简单 Canvas（不用追求完美），先实现节点显示、选择、持久化
- 集成 WebView 编辑器作为后续提升点，初期可用简单文本框实现保存/预览逻辑

示例任务（可直接转 issues）
- 初始化 solution & projects
- 实现 Core 模型与接口
- 实现 SqliteStorageProvider 与 schema 执行
- MainWindow + MapList + 新建地图
- MapCanvas 基本渲染（节点/拖拽）
- KnowledgeCard 存储与简单编辑（文本）
- Links 数据模型 + 卡片下方显示关联
- QuickJump 搜索与跳转

```