namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for actors (entities that can act in combat)
/// </summary>
public interface IActor
{
  int CurrentHealth { get; set; }
  int MaxHealth { get; }
  int CurrentMana { get; set; }
  int MaxMana { get; }
  bool IsAlive { get; }
  void TakeDamage(int amount);
  void Heal(int amount);
}
