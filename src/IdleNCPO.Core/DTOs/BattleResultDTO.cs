using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.DTOs;

/// <summary>
/// Data transfer object for battle result information
/// Used for saving and replaying battles
/// </summary>
public class BattleResultDTO
{
  /// <summary>
  /// Battle seed used for random number generation
  /// </summary>
  public int BattleSeed { get; set; }

  /// <summary>
  /// Loot seed used for item drop generation
  /// </summary>
  public int LootSeed { get; set; }

  /// <summary>
  /// Map key where the battle took place
  /// </summary>
  public EnumMap MapKey { get; set; }

  /// <summary>
  /// Map level when the battle started
  /// </summary>
  public int MapLevel { get; set; }

  /// <summary>
  /// Character data at the start of the battle
  /// </summary>
  public CharacterDTO Player { get; set; } = null!;

  /// <summary>
  /// Total number of ticks the battle took
  /// </summary>
  public int TotalTicks { get; set; }

  /// <summary>
  /// Duration of the battle in seconds (TotalTicks / 30)
  /// </summary>
  public double DurationSeconds => TotalTicks / 30.0;

  /// <summary>
  /// Whether the player won the battle
  /// </summary>
  public bool IsVictory { get; set; }

  /// <summary>
  /// Experience gained from the battle
  /// </summary>
  public int ExperienceGained { get; set; }

  /// <summary>
  /// Items dropped from the battle
  /// </summary>
  public List<ItemDTO> ItemsDropped { get; set; } = new();

  /// <summary>
  /// Timestamp when the battle was completed
  /// </summary>
  public DateTime CompletedAt { get; set; }

  /// <summary>
  /// Create BattleSeedDTO from this result for replay
  /// </summary>
  public BattleSeedDTO ToSeedDTO()
  {
    return new BattleSeedDTO
    {
      BattleSeed = BattleSeed,
      LootSeed = LootSeed,
      MapKey = MapKey,
      MapLevel = MapLevel,
      Player = Player
    };
  }
}
