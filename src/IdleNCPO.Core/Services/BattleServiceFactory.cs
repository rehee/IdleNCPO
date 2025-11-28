using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Core.DTOs;

namespace IdleNCPO.Core.Services;

/// <summary>
/// Factory for creating BattleService instances
/// Implements dependency injection pattern - services that need runtime data should be created via factory
/// </summary>
public class BattleServiceFactory : IBattleServiceFactory<BattleSeedDTO, BattleResultDTO>
{
  private readonly ProfileService _profileService;

  public BattleServiceFactory(ProfileService profileService)
  {
    _profileService = profileService;
  }

  /// <summary>
  /// Create a new battle instance with the given seed
  /// </summary>
  public IBattle CreateBattle(BattleSeedDTO seed)
  {
    return new BattleService(_profileService, seed);
  }
}
