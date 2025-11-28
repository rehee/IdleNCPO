using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Components;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Core.Helpers;
using IdleNCPO.Core.Services;

namespace IdleNCPO.Core.Tests;

public class BattleServiceTests
{
  private readonly ProfileService _profileService;

  public BattleServiceTests()
  {
    _profileService = new ProfileService();
  }

  [Fact]
  public void BattleService_ShouldInitializeCorrectly()
  {
    var seed = CreateTestBattleSeed();
    var battle = new BattleService(_profileService, seed);

    Assert.Equal(seed.BattleSeed, battle.BattleSeed);
    Assert.Equal(seed.LootSeed, battle.LootSeed);
    Assert.Equal(0, battle.CurrentTick);
    Assert.False(battle.IsFinished);
  }

  [Fact]
  public void BattleService_ShouldHavePlayerOnMap()
  {
    var seed = CreateTestBattleSeed();
    var battle = new BattleService(_profileService, seed);

    Assert.NotNull(battle.Map.Player);
    Assert.Equal("TestPlayer", battle.Map.Player.Name);
  }

  [Fact]
  public void BattleService_ShouldSpawnMonsters()
  {
    var seed = CreateTestBattleSeed();
    var battle = new BattleService(_profileService, seed);

    Assert.NotEmpty(battle.Map.Monsters);
    Assert.True(battle.Map.Monsters.All(m => m.IsAlive));
  }

  [Fact]
  public void BattleService_ProcessTick_ShouldIncrementTick()
  {
    var seed = CreateTestBattleSeed();
    var battle = new BattleService(_profileService, seed);

    battle.ProcessTick();

    Assert.Equal(1, battle.CurrentTick);
  }

  [Fact]
  public void BattleService_SameSeed_ShouldProduceSameResults()
  {
    var seed1 = CreateTestBattleSeed(12345, 67890);
    var seed2 = CreateTestBattleSeed(12345, 67890);

    var battle1 = new BattleService(_profileService, seed1);
    var battle2 = new BattleService(_profileService, seed2);

    battle1.RunToCompletion();
    battle2.RunToCompletion();

    Assert.Equal(battle1.CurrentTick, battle2.CurrentTick);
    Assert.Equal(battle1.IsVictory, battle2.IsVictory);
    Assert.Equal(battle1.ExperienceGained, battle2.ExperienceGained);
  }

  [Fact]
  public void BattleService_RunToCompletion_ShouldFinish()
  {
    var seed = CreateTestBattleSeed();
    var battle = new BattleService(_profileService, seed);

    battle.RunToCompletion();

    Assert.True(battle.IsFinished);
    Assert.True(battle.CurrentTick > 0);
  }

  private BattleSeedDTO CreateTestBattleSeed(int? battleSeed = null, int? lootSeed = null)
  {
    var player = new CharacterDTO
    {
      Id = Guid.NewGuid(),
      Name = "TestPlayer",
      Level = 5,
      Strength = 10,
      Dexterity = 10,
      Intelligence = 10,
      Vitality = 10,
      Skills = new List<SkillDTO>
      {
        new SkillDTO
        {
          Id = Guid.NewGuid(),
          SkillType = EnumSkill.Fireball,
          Level = 1
        },
        new SkillDTO
        {
          Id = Guid.NewGuid(),
          SkillType = EnumSkill.BasicAttack,
          Level = 1
        }
      }
    };

    return BattleSeedHelper.CreateBattleSeed(
      player,
      EnumMap.StarterVillage,
      1,
      1,
      battleSeed,
      lootSeed);
  }
}
