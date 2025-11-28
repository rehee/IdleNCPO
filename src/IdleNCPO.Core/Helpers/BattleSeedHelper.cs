using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.DTOs;

namespace IdleNCPO.Core.Helpers;

/// <summary>
/// Helper class for battle seed generation
/// </summary>
public static class BattleSeedHelper
{
  private static readonly Random _seedRandom = new();

  public static BattleSeedDTO CreateBattleSeed(
    CharacterDTO player,
    EnumMap mapKey,
    int difficulty = 1,
    int mapLevel = 1,
    int? battleSeed = null,
    int? lootSeed = null)
  {
    return new BattleSeedDTO
    {
      BattleSeed = battleSeed ?? _seedRandom.Next(),
      LootSeed = lootSeed ?? _seedRandom.Next(),
      Player = player,
      MapKey = mapKey,
      Difficulty = difficulty,
      MapLevel = mapLevel
    };
  }

  public static BattleSeedDTO CreateReplaySeed(
    CharacterDTO player,
    EnumMap mapKey,
    int difficulty,
    int mapLevel,
    int battleSeed,
    int lootSeed)
  {
    return new BattleSeedDTO
    {
      BattleSeed = battleSeed,
      LootSeed = lootSeed,
      Player = player,
      MapKey = mapKey,
      Difficulty = difficulty,
      MapLevel = mapLevel
    };
  }
}
