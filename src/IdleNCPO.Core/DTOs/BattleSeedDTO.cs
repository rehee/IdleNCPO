using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.DTOs;

/// <summary>
/// Data transfer object for battle seed information
/// </summary>
public class BattleSeedDTO
{
  public int BattleSeed { get; set; }
  public int LootSeed { get; set; }
  public CharacterDTO Player { get; set; } = null!;
  public EnumMap MapKey { get; set; }
  public int Difficulty { get; set; }
  public int MapLevel { get; set; }
}
