using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

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
/// </summary>
public abstract class MapIdleProfile : IdleProfile<EnumMap>
{
  public abstract int Width { get; }
  public abstract int Height { get; }
  public abstract int MinLevel { get; }
  public abstract int MaxLevel { get; }
  public abstract List<WaveDefinition> Waves { get; }
}

/// <summary>
/// Profile for Starter Village map
/// </summary>
public class StarterVillageMapProfile : MapIdleProfile
{
  public override EnumMap Key => EnumMap.StarterVillage;
  public override string Name => "新手村";
  public override string Description => "适合初学者的安全区域";
  public override int Width => 10;
  public override int Height => 10;
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
  public override int Width => 15;
  public override int Height => 15;
  public override int MinLevel => 5;
  public override int MaxLevel => 10;
  public override List<WaveDefinition> Waves => new()
  {
    new() { WaveNumber = 1, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 3 } } },
    new() { WaveNumber = 2, Monsters = new() { new() { MonsterType = EnumMonster.Spider, Count = 4 } } },
    new() { WaveNumber = 3, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 2 }, new() { MonsterType = EnumMonster.Spider, Count = 2 } } }
  };
}
