using IdleNCPO.Abstractions.Entities;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Entity for storing battle replay information
/// </summary>
public class BattleReplayEntity : BaseEntity
{
  public Guid PlayerId { get; set; }
  public EnumMap MapKey { get; set; }
  public int Difficulty { get; set; }
  public int BattleSeed { get; set; }
  public int LootSeed { get; set; }
  public int TotalTicks { get; set; }
  public bool IsVictory { get; set; }
  public int ExperienceGained { get; set; }
  public List<Guid> ItemsDropped { get; set; } = new();
}
