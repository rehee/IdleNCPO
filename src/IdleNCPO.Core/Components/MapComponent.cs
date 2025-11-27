using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Components;

/// <summary>
/// Component for map runtime data
/// </summary>
public class MapComponent : BaseComponent<EnumMap>
{
  public override EnumMap ProfileKey { get; protected set; }
  
  public int Width { get; set; }
  public int Height { get; set; }
  public int CurrentWave { get; set; }
  public int TotalWaves { get; set; }
  public List<MonsterComponent> Monsters { get; set; } = new();
  public CharacterComponent? Player { get; set; }

  public MapComponent(EnumMap profileKey)
  {
    ProfileKey = profileKey;
  }

  public bool IsWaveComplete => Monsters.All(m => !m.IsAlive);
  public bool IsMapComplete => IsWaveComplete && CurrentWave >= TotalWaves;

  public List<MonsterComponent> GetAliveMonsters()
  {
    return Monsters.Where(m => m.IsAlive).ToList();
  }
}
