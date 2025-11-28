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

  /// <summary>
  /// 2D game map for spatial management
  /// </summary>
  public GameMap2D? GameMap { get; private set; }

  public MapIdleComponent(EnumMap profileKey)
  {
    ProfileKey = profileKey;
  }

  /// <summary>
  /// Initialize the 2D game map
  /// </summary>
  public void InitializeGameMap()
  {
    GameMap = new GameMap2D(Width, Height);
    
    if (Player != null)
    {
      GameMap.AddActor(Player);
    }
    
    foreach (var monster in Monsters)
    {
      GameMap.AddActor(monster);
    }
  }

  /// <summary>
  /// Update game map when monsters are spawned
  /// </summary>
  public void UpdateGameMapActors()
  {
    if (GameMap == null) return;

    // Re-add all actors
    if (Player != null)
    {
      GameMap.AddActor(Player);
    }
    
    foreach (var monster in Monsters)
    {
      GameMap.AddActor(monster);
    }
  }

  public bool IsWaveComplete => Monsters.All(m => !m.IsAlive);
  public bool IsMapComplete => IsWaveComplete && CurrentWave >= TotalWaves;

  public List<MonsterIdleComponent> GetAliveMonsters()
  {
    return Monsters.Where(m => m.IsAlive).ToList();
  }
}
