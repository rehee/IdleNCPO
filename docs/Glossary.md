# IdleNCPO 术语表 (Glossary)

本文档定义了 IdleNCPO 项目中使用的术语，以便于团队成员和 AI 助手之间的沟通。

## 核心概念 (Core Concepts)

### IdleProfile (配置/模板)
- **定义**: 游戏对象的静态定义模板（使用 Idle 前缀以避免命名冲突）
- **用途**: 定义游戏对象的类型、基础属性和行为
- **特点**: 
  - 每个 IdleProfile 有一个唯一的枚举 Key
  - 使用枚举约束类型安全
  - 是只读的静态数据
  - 使用抽象类继承模式，每个具体类型对应一个枚举值
- **实现模式**:
  - `IdleProfile<T>` 作为基类
  - `MonsterIdleProfile` 作为怪物的抽象基类
  - `SkeletonMonsterProfile : MonsterIdleProfile` 具体实现类
- **示例**: MonsterIdleProfile, MapIdleProfile, SkillIdleProfile, ItemIdleProfile

### IdleComponent (组件)
- **定义**: 游戏对象的运行时实例（使用 Idle 前缀以避免命名冲突）
- **用途**: 保存游戏运行时的临时状态数据
- **特点**:
  - 通过 Key 与 IdleProfile 关联
  - 包含动态变化的数据（如当前生命值）
- **示例**: MapIdleComponent, CharacterIdleComponent, MonsterIdleComponent

### Entity (实体)
- **定义**: 持久化存储的数据对象
- **用途**: 保存需要持久化到数据库的数据
- **特点**:
  - 与数据库表对应
  - 不包含游戏逻辑
- **示例**: ActorEntity, ItemEntity, SkillEntity

### DTO (数据传输对象)
- **定义**: 用于数据传输的中间对象
- **用途**: 在 Entity 和 IdleComponent 之间传递数据
- **流向**: Entity + IdleProfile → DTO → IdleComponent (单向)

## 游戏元素 (Game Elements)

### Map (地图)
- **定义**: 2D 战斗场景
- **特点**:
  - 按波次 (Wave) 生成怪物
  - 不同地图有不同的怪物配置

### Wave (波次)
- **定义**: 地图中怪物的出现批次
- **特点**: 当前波次怪物全部消灭后出现下一波

### Actor (角色)
- **定义**: 游戏中可行动的实体
- **包括**: 玩家角色、怪物

### Character (玩家角色)
- **定义**: 玩家控制的角色
- **特点**: 可装备物品、使用技能

### Monster (怪物)
- **定义**: 敌对的非玩家角色
- **特点**: 被击杀可获得经验和物品

### Item (物品)
- **定义**: 游戏中所有可拾取的物品的基类
- **分类**:
  - Equipment (装备): 可穿戴的物品
    - Weapon (武器): 提升攻击
    - Armor (防具): 提供防护
    - Accessory (饰品): 特殊效果
  - Junk (垃圾): 只能出售的物品

### Equipment (装备)
- **定义**: 可穿戴的物品，是 Item 的子类
- **用途**: 提升角色属性
- **分类**: 武器、护甲、饰品等

### Skill (技能)
- **定义**: 角色可使用的能力
- **特点**: 可被辅助技能 (Support Skill) 修改

### Support Skill (辅助技能)
- **定义**: 用于修改主技能效果的技能
- **用途**: 增强、改变技能的行为

## 战斗系统 (Combat System)

### Battle Seed (战斗种子)
- **定义**: 用于生成战斗随机数的种子
- **用途**: 确保战斗可重放

### Loot Seed (掉落种子)
- **定义**: 用于生成物品掉落的随机数种子
- **特点**: 只在首次战斗时使用

### Tick (游戏刻)
- **定义**: 游戏逻辑的最小时间单位
- **用途**: 战斗计算的基本时间步长
- **频率**: 30 ticks = 1 秒 (即每秒30帧)
- **播放**: 客户端播放回放时，每 tick 等待 1/30 秒

### Replay (回放)
- **定义**: 战斗的可重放记录
- **内容**: 种子信息、Tick 数、战斗结果、战斗耗时
- **特点**: 回放不产生奖励
- **实现方式**: 
  - 服务器端/客户端战斗时即时计算完成并保存战斗种子
  - 播放时使用相同种子重新计算，每 tick 等待 1/30 秒显示动画

### Strategy (策略)
- **定义**: 玩家设置的自动行为规则
- **包括**: 目标优先级、技能使用条件

## 技术术语 (Technical Terms)

### Repository (仓储)
- **定义**: 数据访问的抽象接口
- **用途**: 统一实体的增删改查操作

### Offline Mode (单机模式)
- **定义**: 本地运行的游戏模式
- **特点**: 所有运算在本地，不做服务器验证

### Online Mode (在线模式)
- **定义**: 连接服务器的游戏模式
- **特点**: 需要用户登录，服务器端验证

## 枚举类型 (Enum Types)

### 命名规则
- 前缀: `Enum`
- 格式: `Enum[类名]`
- 默认值: `NotSpecified = 0`

### 示例
- `EnumMap`: 地图类型枚举
- `EnumMonster`: 怪物类型枚举
- `EnumSkill`: 技能类型枚举
- `EnumItem`: 物品类型枚举
- `EnumItemCategory`: 物品分类枚举 (Equipment, Junk)
- `EnumEquipmentSlot`: 装备槽位枚举
- `EnumAttribute`: 属性类型枚举
