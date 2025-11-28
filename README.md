# IdleNCPO

一个以暗黑破坏神、流放之路和 Hall of Fame (HOF) 为灵感的放置类游戏框架。

## 项目概述

IdleNCPO 是一个架构合理的游戏框架，支持单机和在线模式。玩家可以创建角色、选择地图、与怪物战斗获得经验和装备。

## 技术栈

- **框架**: .NET 8
- **Web 客户端**: Blazor WASM + BootstrapBlazor
- **桌面应用**: WPF Blazor 混合应用
- **移动应用**: MAUI Blazor 混合应用
- **服务器**: ASP.NET Core Web API
- **数据库**: PostgreSQL (服务器端) / IndexedDB (客户端)
- **身份验证**: JWT Token
- **容器化**: Docker

## 项目结构

```
IdleNCPO/
├── src/
│   ├── IdleNCPO.Abstractions/    # 抽象接口和基类
│   ├── IdleNCPO.Core/            # 核心游戏逻辑
│   ├── IdleNCPO.Data/            # 数据访问层
│   ├── IdleNCPO.Server/          # API 服务器
│   ├── IdleNCPO.Web/             # Blazor WASM 客户端
│   ├── IdleNCPO.Desktop/         # WPF Blazor 混合应用
│   ├── IdleNCPO.Mobile/          # MAUI Blazor 混合应用
│   └── IdleNCPO.Shared/          # 共享 Razor 类库
├── tests/
│   ├── IdleNCPO.Abstractions.Tests/
│   ├── IdleNCPO.Core.Tests/
│   └── IdleNCPO.Data.Tests/
├── docs/
│   └── Glossary.md               # 术语文档
├── rule.md                       # 开发规范
└── docker-compose.yml            # Docker 配置
```

## 核心架构

### Profile-Component-Entity 模式

- **Profile**: 游戏对象的静态定义模板（如地图配置、怪物属性）
- **Component**: 游戏运行时的实例数据（如角色当前状态）
- **Entity**: 持久化存储的数据对象

### 战斗系统

- 使用随机数种子确保战斗可重放
- 两个独立种子：战斗种子和掉落种子
- 支持服务器端快速结算和客户端重放

## 快速开始

### 前置条件

- .NET 8 SDK
- Docker (可选，用于运行 PostgreSQL)

### 运行开发环境

```bash
# 克隆仓库
git clone https://github.com/rehee/IdleNCPO.git
cd IdleNCPO

# 构建项目
dotnet build

# 运行测试
dotnet test

# 运行 Web 客户端
cd src/IdleNCPO.Web
dotnet run

# 运行服务器 (另一个终端)
cd src/IdleNCPO.Server
dotnet run
```

### 使用 Docker

```bash
# 启动 PostgreSQL 和服务器
docker-compose up -d
```

## 游戏特性

- 🎮 创建角色并选择地图进行战斗
- ⚔️ 击杀怪物获得经验和装备
- 📈 升级角色提升属性
- 🗺️ 2D 波次地图系统
- 🔄 战斗可重放（通过种子）
- 📱 支持多平台（Web、桌面、移动）

## 开发规范

详见 [rule.md](rule.md) 文件。

### 主要规范

- 一个 `.cs` 文件只包含一个类
- 缩进使用 2 个空格
- Enum 以 `Enum` 前缀开头，默认值为 `NotSpecified = 0`

## 文档

- [开发规范](rule.md)
- [术语表](docs/Glossary.md)

## License

MIT