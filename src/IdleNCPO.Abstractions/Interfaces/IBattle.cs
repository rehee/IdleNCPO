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
  bool IsVictory { get; }
  int MaxTicks { get; }
  int ExperienceGained { get; }
  void ProcessTick();
  void RunToCompletion();
  void RunToCompletion(int maxTicks);
}
