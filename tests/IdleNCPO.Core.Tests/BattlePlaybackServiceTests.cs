using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Core.Helpers;
using IdleNCPO.Core.Profiles;
using IdleNCPO.Core.Services;

namespace IdleNCPO.Core.Tests;

public class BattlePlaybackServiceTests
{
  private readonly ProfileService _profileService;
  private readonly BattleServiceFactory _battleFactory;

  public BattlePlaybackServiceTests()
  {
    _profileService = new ProfileService();
    _battleFactory = new BattleServiceFactory(_profileService);
  }

  [Fact]
  public void BattlePlaybackService_ShouldInitializeCorrectly()
  {
    var playbackService = new BattlePlaybackService(_battleFactory);

    Assert.False(playbackService.IsPlaying);
    Assert.Null(playbackService.Battle);
    Assert.Equal(MapIdleProfile.TicksPerSecond, playbackService.TicksPerSecond);
  }

  [Fact]
  public void BattlePlaybackService_TickDelayMs_ShouldCalculateCorrectly()
  {
    var playbackService = new BattlePlaybackService(_battleFactory);

    // At 30 ticks per second, delay should be ~33ms
    Assert.Equal(1000 / 30, playbackService.TickDelayMs);
  }

  [Fact]
  public async Task BattlePlaybackService_StartPlayback_ShouldPlayBattle()
  {
    var playbackService = new BattlePlaybackService(_battleFactory);
    var result = CreateTestBattleResult();
    var ticksProcessed = 0;

    playbackService.OnTickProcessed += _ => ticksProcessed++;
    playbackService.TicksPerSecond = 1000; // Speed up for testing

    await playbackService.StartPlaybackAsync(result);

    Assert.Equal(result.TotalTicks, ticksProcessed);
  }

  [Fact]
  public async Task BattlePlaybackService_StartPlayback_ShouldFireCompleteEvent()
  {
    var playbackService = new BattlePlaybackService(_battleFactory);
    var result = CreateTestBattleResult();
    var completeCalled = false;

    playbackService.OnPlaybackComplete += _ => completeCalled = true;
    playbackService.TicksPerSecond = 1000; // Speed up for testing

    await playbackService.StartPlaybackAsync(result);

    Assert.True(completeCalled);
    Assert.False(playbackService.IsPlaying);
  }

  [Fact]
  public async Task BattlePlaybackService_StopPlayback_ShouldStopPlaying()
  {
    var playbackService = new BattlePlaybackService(_battleFactory);
    var result = CreateTestBattleResult();

    // Start in background
    var playTask = playbackService.StartPlaybackAsync(result);

    // Stop immediately
    await playbackService.StopPlaybackAsync();

    Assert.False(playbackService.IsPlaying);
  }

  private BattleResultDTO CreateTestBattleResult()
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
        }
      }
    };

    var seed = BattleSeedHelper.CreateBattleSeed(
      player,
      EnumMap.StarterVillage,
      1,
      1,
      12345,
      67890);

    var battle = new BattleService(_profileService, seed);
    battle.RunToCompletion();

    return battle.GetBattleResult();
  }
}
