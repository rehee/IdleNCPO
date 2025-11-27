namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for battle system
/// </summary>
public interface IBattle
{
  int BattleSeed { get; }
  int LootSeed { get; }
  int CurrentTick { get; }
  bool IsFinished { get; }
  void ProcessTick();
}
