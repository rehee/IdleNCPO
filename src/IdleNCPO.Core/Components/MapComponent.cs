using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for map runtime data
/// </summary>
public class MapIdleComponent : IdleComponent<EnumMap>
{
  public override EnumMap ProfileKey { get; protected set; }
  
  public int Width { get; set; }
  public int Height { get; set; }
  public int CurrentWave { get; set; }
  public int TotalWaves { get; set; }
  public List<MonsterIdleComponent> Monsters { get; set; } = new();
  public CharacterIdleComponent? Player { get; set; }

  public MapIdleComponent(EnumMap profileKey)
  {
    ProfileKey = profileKey;
  }

  public bool IsWaveComplete => Monsters.All(m => !m.IsAlive);
  public bool IsMapComplete => IsWaveComplete && CurrentWave >= TotalWaves;

  public List<MonsterIdleComponent> GetAliveMonsters()
  {
    return Monsters.Where(m => m.IsAlive).ToList();
  }
}
