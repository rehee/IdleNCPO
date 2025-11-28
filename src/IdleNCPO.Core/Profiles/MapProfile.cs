using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;
using IdleNCPO.Core.Components;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Definition of a wave of monsters
/// </summary>
public class WaveDefinition
{
  public int WaveNumber { get; set; }
  public List<MonsterSpawn> Monsters { get; set; } = new();
}

/// <summary>
/// Definition of monster spawn in a wave
/// </summary>
public class MonsterSpawn
{
  public EnumMonster MonsterType { get; set; }
  public int Count { get; set; }
  public int LevelModifier { get; set; }
}

/// <summary>
/// Abstract base profile for all map types
/// Map default: 20x20 units as per requirements
/// </summary>
public abstract class MapIdleProfile : IdleProfile<EnumMap>
{
  /// <summary>
  /// Default max battle duration in seconds
  /// </summary>
  public const int DefaultMaxBattleDuration = 300;

  /// <summary>
  /// Ticks per second for battle simulation
  /// </summary>
  public const int TicksPerSecond = 30;

  public virtual int Width => GameMap2D.DefaultWidth;
  public virtual int Height => GameMap2D.DefaultHeight;
  public abstract int MinLevel { get; }
  public abstract int MaxLevel { get; }
  public abstract List<WaveDefinition> Waves { get; }

  /// <summary>
  /// Maximum battle duration in seconds
  /// </summary>
  public virtual int MaxBattleDuration => DefaultMaxBattleDuration;

  /// <summary>
  /// Maximum ticks for this battle (MaxBattleDuration * TicksPerSecond)
  /// </summary>
  public int MaxTicks => MaxBattleDuration * TicksPerSecond;
}

/// <summary>
/// Profile for Starter Village map
/// </summary>
public class StarterVillageMapProfile : MapIdleProfile
{
  public override EnumMap Key => EnumMap.StarterVillage;
  public override string Name => "新手村";
  public override string Description => "适合初学者的安全区域";
  public override int MinLevel => 1;
  public override int MaxLevel => 5;
  public override List<WaveDefinition> Waves => new()
  {
    new() { WaveNumber = 1, Monsters = new() { new() { MonsterType = EnumMonster.Skeleton, Count = 2 } } },
    new() { WaveNumber = 2, Monsters = new() { new() { MonsterType = EnumMonster.Skeleton, Count = 3 } } }
  };
}

/// <summary>
/// Profile for Dark Cave map
/// </summary>
public class DarkCaveMapProfile : MapIdleProfile
{
  public override EnumMap Key => EnumMap.DarkCave;
  public override string Name => "黑暗洞窟";
  public override string Description => "危险的地下洞穴";
  public override int MinLevel => 5;
  public override int MaxLevel => 10;
  public override List<WaveDefinition> Waves => new()
  {
    new() { WaveNumber = 1, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 3 } } },
    new() { WaveNumber = 2, Monsters = new() { new() { MonsterType = EnumMonster.Spider, Count = 4 } } },
    new() { WaveNumber = 3, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 2 }, new() { MonsterType = EnumMonster.Spider, Count = 2 } } }
  };
}
