using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for monster runtime data
/// </summary>
public class MonsterIdleComponent : IdleComponent<EnumMonster>, IMapActor
{
  public override EnumMonster ProfileKey { get; protected set; }
  
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; }
  public int CurrentHealth { get; set; }
  public int MaxHealth { get; set; }
  public int CurrentMana { get; set; }
  public int MaxMana { get; set; }
  public int BaseDamage { get; set; }
  public int BaseArmor { get; set; }
  public int BaseExperience { get; set; }
  public EnumDamageType DamageType { get; set; }
  public bool IsAlive => CurrentHealth > 0;
  
  // Legacy integer position (kept for backward compatibility)
  public int X { get; set; }
  public int Y { get; set; }

  // IMapActor implementation - 2D position with double precision
  public Position2D Position { get; set; }
  public double Radius => GameMap2D.StandardRadius;
  public double MoveSpeed { get; set; } = 0.08;
  public IMapActor? MoveTarget { get; set; }
  public bool IsColliding { get; set; }

  public MonsterIdleComponent(EnumMonster profileKey)
  {
    ProfileKey = profileKey;
    Id = Guid.NewGuid();
  }

  public void TakeDamage(int amount)
  {
    var actualDamage = Math.Max(1, amount - BaseArmor);
    CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
  }

  public void Heal(int amount)
  {
    CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
  }

  public int GetExperienceValue()
  {
    return BaseExperience + (Level * 10);
  }

  /// <summary>
  /// Calculate distance to another actor (center to center minus both radii)
  /// </summary>
  public double DistanceTo(IMapActor other)
  {
    var centerDistance = Position.DistanceTo(other.Position);
    return centerDistance - Radius - other.Radius;
  }

  /// <summary>
  /// Get the grid cell this actor occupies
  /// </summary>
  public (int GridX, int GridY) GetGridCell()
  {
    return Position.GetGridCell();
  }

  /// <summary>
  /// Move towards the target position by speed amount
  /// </summary>
  public void MoveTowards(Position2D target, double speed)
  {
    var direction = Position.DirectionTo(target);
    var distance = Position.DistanceTo(target);
    
    if (distance <= speed)
    {
      Position = target;
    }
    else
    {
      Position = Position + direction * speed;
    }
  }
}
