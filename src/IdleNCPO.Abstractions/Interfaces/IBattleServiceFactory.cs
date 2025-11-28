namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Factory interface for creating battle service instances
/// Follows SOLID principle - use factory pattern for creating instances that require runtime data
/// </summary>
/// <typeparam name="TSeed">Seed data type for battle initialization</typeparam>
/// <typeparam name="TResult">Result data type for battle outcome</typeparam>
public interface IBattleServiceFactory<TSeed, TResult>
{
  /// <summary>
  /// Create a new battle instance with the given seed
  /// </summary>
  IBattle CreateBattle(TSeed seed);
}
