# MindMapTool Architecture 设计（中文）

版本：v0.1
作者：lan257

概述
- 目标：重构并实现一个本地单机可运行的思维导图工具，支持节点的知识卡片（Markdown 富文本）、跨思维导图节点关联与快捷跳转，且具备后端可扩展性。
- 核心设计原则：简洁可用（MVP）、模块化（接口驱动）、可迁移（数据模型兼容 Neo4j/Postgres）、可扩展（插件/Adapter）。

技术栈建议（MVP）
- 桌面框架：Avalonia（跨平台）
- UI 模式：CommunityToolkit.Mvvm
- UI 组件/样式：Semi / Ursa
- 本地存储：SQLite（通过 SQLSugar 做数据访问层）
- Markdown 编辑/渲染：嵌入 WebView + Toast UI Editor（优先）
- 本地搜索：Lucene.NET（可选，便于离线模糊搜索）
- 索引/全文检索：后端可接入 Elasticsearch（你熟悉）
- 后端扩展：REST API 方案（ASP.NET Core WebAPI）
- 图数据库（进阶）：Neo4j（用于复杂关系查询与可视化优化）

核心模块（高层）
1. UI 层（Avalonia）
   - MainWindow: 地图列表、画布、侧栏（属性 / 知识卡片）、底部状态栏
   - MapCanvas: 渲染思维导图节点与连线（支持缩放/拖拽）
   - NodeInspector: 节点属性 + 打开知识卡片按钮
   - KnowledgeCard: Markdown 编辑/预览 + 关联管理
   - QuickJumpOverlay: 全局跳转/书签/历史

2. 应用层（MVVM）
   - ViewModels: MapListVM, MapEditorVM, NodeVM, KnowledgeCardVM, QuickJumpVM
   - Commands + Navigation Service

3. 服务层
   - IStorageProvider（CRUD for Maps/Nodes/Cards/Links）
   - IGraphProvider（图相关查询：查邻居、短路径等；MVP 可使用 SQLite 实现）
   - ISearchProvider（全文检索）
   - IPluginManager（插件加载与生命周期）

4. 数据访问层（DAL）
   - SQLSugar-based repository 实现（SQLite）
   - 提供事务、批量导入导出、版本迁移脚本

5. 后端适配（可选）
   - WebAPI Adapter：实现同样的接口（IStorageProvider 等）通过 HTTP 调用后端服务
   - 同时客户端保持本地缓存/离线模式并做同步策略（后续）

数据模型（概要；后面有 schema.sql）
- Map: id (GUID), title, description, created_at, updated_at, meta (JSON)
- Node: id (GUID), map_id, parent_id (nullable), title, position_x, position_y, order, created_at, updated_at
- KnowledgeCard: id (GUID), node_id (FK), content_markdown (TEXT), content_html (cached), language, tags (JSON/CSV), created_at, updated_at
- Link: id (GUID), source_node_id, target_node_id, type, title, created_at
- NodeReference: global ref format (map_id + node_id or absolute GUID)
- MapTheme / Settings: 保存主题、语言、用户偏好

跨地图关联与跳转
- 存储 Link 表：存 source_node_id -> target_node_id（target 可以是其它 map）
- 在知识卡片下方列出关联 Link（带小预览），点击触发导航服务：
  1) 加载目标 Map（如果未打开则打开）
  2) 定位并高亮目标 Node
  3) 可选：在侧栏显示目标 Node 的知识卡片摘要
- 快捷跳转：实现全局快捷键（例如 Ctrl+K 或 Ctrl+P）弹出 QuickJumpOverlay，
  支持模糊搜索 Node title、Map title、Tag、Card 内容（借助 Lucene.NET 或 SQL LIKE）

离线/同步 & 后端迁移策略
- 本地首次使用：全部数据保存在 SQLite 文件（config 存储文件路径）
- 为支持未来后端：设计 Storage Provider 接口，所有操作只通过接口进行
- 数据迁移：
  - SQLite -> PostgreSQL: 可逐表迁移，保留 GUID 主键
  - 如果想用 Neo4j：将 Node 与 Link 导出成关系边（source_node_id -> target_node_id），保留 node metadata。写一个迁移工具（导出 JSON -> Neo4j bulk import）
- 搜索：本地使用 Lucene.NET，后端使用 Elasticsearch；设计 IndexingService 以便同时支持两种

插件与扩展点
- 插件点：
  - 存储实现（SQLite / PostgreSQL / Neo4j / Remote API）
  - 编辑器：替换 Markdown 编辑器
  - 导入/导出格式：OPML, JSON, Markdown bundle
  - 可视化/布局算法（radial / tree / force）
- 插件加载：读取 plugins 文件夹并通过反射加载实现 IPlugin 接口的 DLL

安全与备份
- 提供导出（单文件 backup.zip 包含 sqlite + attachments + metadata）
- 自动备份（可配置）
- 可选：加密数据库文件（后续）

性能考虑
- 大量节点时：Canvas 采用虚拟化渲染、按需渲染
- 查询性能：常用查询（按标题/标签/ID）加索引
- 关联查询：若需要复杂图算法，尽早接入 Neo4j 以提升响应速度

API 设计示例（用于后端适配）
- GET /api/maps
- GET /api/maps/{mapId}
- POST /api/maps
- GET /api/maps/{mapId}/nodes
- POST /api/maps/{mapId}/nodes
- GET /api/nodes/{nodeId}/card
- POST /api/nodes/{nodeId}/card
- GET /api/nodes/search?q=...
- POST /api/links (建立跨图链接)
- DELETE /api/links/{linkId}

MVP 功能清单（最小可交付）
1. 本地新建/保存/删除思维导图（SQLite）
2. 添加/编辑/删除节点（带位置与层级）
3. 每节点知识卡片（Markdown 编辑 + 预览，增删改查）
4. 跨图的节点关联（创建/编辑/删除 Link）
5. 全局跳转面板（按键弹出，支持搜索并跳转）
6. 导入/导出 JSON（后续支持 OPML）
7. 简单主题设置（深色/浅色）

后续可选特性（优先级可调）
- 离线全文检索（Lucene.NET）
- 多端同步（WebAPI + OAuth）
- 高级图算法（路径/相似度、知识图谱）
- 可视化布局与导出为图片/PDF
- 插件市场

结束语
本设计以“从可用的单机工具逐步扩展到后端服务”为目标。核心策略是接口驱动（Storage/Graph/Search），MVP 时用 SQLite + Avalonia+嵌入式 Markdown 编辑器快速交付。后续将数据与索引替换为 PostgreSQL/Neo4j/ES 可无缝迁移。