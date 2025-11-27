using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Profile for map types
/// </summary>
public class MapProfile : BaseProfile<EnumMap>
{
  public override EnumMap Key { get; }
  public override string Name { get; }
  public override string Description { get; }
  public int Width { get; }
  public int Height { get; }
  public int MinLevel { get; }
  public int MaxLevel { get; }
  public List<WaveDefinition> Waves { get; }

  public MapProfile(
    EnumMap key,
    string name,
    string description,
    int width,
    int height,
    int minLevel,
    int maxLevel,
    List<WaveDefinition>? waves = null)
  {
    Key = key;
    Name = name;
    Description = description;
    Width = width;
    Height = height;
    MinLevel = minLevel;
    MaxLevel = maxLevel;
    Waves = waves ?? new List<WaveDefinition>();
  }
}

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
