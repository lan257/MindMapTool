# Roadmap & Implementation Plan（中文）

阶段 0：准备（1-2 天）
- 创建项目骨架：Avalonia app + MVVM 基础
- 初始化 SQLSugar 与 SQLite 文件
- 建立基本 DI（Microsoft.Extensions.DependencyInjection）
- 添加 GitHub 仓库 README（包括此设计文档）

阶段 1：MVP 核心（2-3 周）
目标：实现可用的单机版并能演示主要交互
任务：
- Map CRUD（列表、新建、删除、打开）
- Canvas：基本节点渲染、拖拽、缩放（支持简单树布局）
- Node CRUD（增删改查）
- KnowledgeCard 编辑器（Markdown，采用 WebView + Toast UI 的内嵌）
- Links（跨图链接的 CRUD）
- QuickJump：按键弹出，按标题/标签搜索并跳转
- 导入/导出 JSON
- 单元测试与基本集成测试

阶段 2：稳定性与性能（1-2 周）
- 本地搜索（Lucene.NET）或 SQLite FTS
- 虚拟化渲染优化
- 自动备份与导出功能
- UI/UX 调整（主题、语言切换）

阶段 3：扩展与后端接入（2-4 周）
- 抽象 IStorageProvider 并实现 Remote Adapter（WebAPI）
- 编写迁移工具（SQLite -> PostgreSQL / Neo4j）
- 增加同步策略（简单的 push/pull）

阶段 4：高级特性（可选，按需）
- Neo4j 作为图分析后端（路径搜索、相似度推荐）
- 插件系统与社区扩展
- 导出图片/PDF、高级布局算法
- 多端（Web/Android）前端重用 WebAPI

估时总计（MVP to Phase2）：约 4-6 周（单人开发、每周 20-30 小时）

建议的第一个开发冲刺（Sprint 0 - 一周内）
- 初始化仓库与 CI（build/publish）
- 完成 schema + SQL 初始化 + 基础 DAL（SQLSugar）
- 实现 Map 列表页面与新建/打开功能
- 实现 Node 的基本模型与简单 Canvas 渲染（不必完美）