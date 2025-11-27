# IdleNCPO 开发规范 (Development Rules)

## 项目概述 (Project Overview)
IdleNCPO 是一个以暗黑破坏神、流放之路和 Hall of Fame (HOF) 为基础的放置类游戏。

## 编码规范 (Coding Standards)

### 文件组织 (File Organization)
- 一个 `.cs` 文件只能包含一个类
- 缩进使用 2 个空格
- Enum 类放在 `Enums` 目录下
- Attribute 类放在 `Attributes` 目录下
- Helper 和扩展方法放在 `Helpers` 目录下

### 命名规范 (Naming Conventions)
- Enum 类以 `Enum` 前缀开头 (例如: `EnumMap`, `EnumMonster`)
- Enum 默认值为 `NotSpecified = 0`
- Enum 命名格式: `Enum[类名]`，尽量不带后缀

### 类型转换 (Type Conversion)
- 类型转换方法应放在 Helper 中

## 架构设计 (Architecture Design)

### Profile-Component-Entity 模式
- **Profile**: 保存类型和逻辑的定义（静态数据）
  - 每个 Profile 有一个 `int` 类型的 Key
  - 使用泛型约束 `where T : Enum`
- **Component**: 临时保存运行时数据的实例
  - 通过 Profile 的 Key 与 Profile 关联
- **Entity**: 持久化数据

### 实例化流程
```
Entity + Profile → DTO → Component
```
注意：不能从 Component 反向返回 DTO 或 Entity

### 战斗系统
- 战斗逻辑与渲染逻辑分离
- 使用随机数种子确保战斗可重放
- 需要两个随机数种子：
  1. 战斗种子：用于战斗计算
  2. 掉落种子：用于物品掉落
- 服务器端战斗可直接结算，保存 Replay 数据
- 只有首次战斗有物品和经验奖励

## 项目结构 (Project Structure)

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
└── docs/
    └── Glossary.md               # 术语文档
```

## 技术栈 (Tech Stack)
- .NET 8
- BootstrapBlazor (UI 框架)
- Blazor WASM (Web)
- WPF Blazor 混合 (Desktop)
- MAUI Blazor 混合 (Mobile)
- ASP.NET Core API (Server)
- Entity Framework Core
- PostgreSQL (Server 端数据库)
- IndexedDB (Client 端持久化)
- JWT Token (身份验证)
- Docker (PostgreSQL 部署)

## 测试要求 (Testing Requirements)
- 除 Razor 类库和展示项目外，所有项目都需要单元测试
- 测试覆盖核心逻辑

## 游戏设计 (Game Design)

### 核心元素
- 地图 (Map): 2D 地图，按波次出现怪物
- 玩家 (Player/Character): 可创建角色，选择地图战斗
- 怪物 (Monster): 不同地图有不同的怪物组合
- 装备 (Equipment): 击杀怪物获得，穿戴提升属性
- 技能 (Skill): 可被辅助技能修改

### 游戏模式
- 单机模式：所有运算和数据在本地，不做验证
- 在线模式：在服务器游玩，需要用户登录
