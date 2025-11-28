using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for character runtime data
/// </summary>
public class CharacterIdleComponent : IdleComponent<EnumMonster>, IMapActor
{
  public override EnumMonster ProfileKey { get; protected set; }
  
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; }
  public int Experience { get; set; }
  public int Strength { get; set; }
  public int Dexterity { get; set; }
  public int Intelligence { get; set; }
  public int Vitality { get; set; }
  public int CurrentHealth { get; set; }
  public int MaxHealth => Vitality * 10 + 50;
  public int CurrentMana { get; set; }
  public int MaxMana => Intelligence * 5 + 30;
  public bool IsAlive => CurrentHealth > 0;
  
  // Legacy integer position (kept for backward compatibility)
  public int X { get; set; }
  public int Y { get; set; }
  
  // IMapActor implementation - 2D position with double precision
  public Position2D Position { get; set; }
  public double Radius => GameMap2D.StandardRadius;
  public double MoveSpeed { get; set; } = 0.1;
  public IMapActor? MoveTarget { get; set; }
  public bool IsColliding { get; set; }
  
  public List<SkillIdleComponent> Skills { get; set; } = new();
  public List<ItemIdleComponent> Equipment { get; set; } = new();

  public void TakeDamage(int amount)
  {
    var actualDamage = Math.Max(1, amount - GetArmor());
    CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
  }

  public void Heal(int amount)
  {
    CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
  }

  public int GetArmor()
  {
    return Equipment.Sum(e => e.GetAttributeValue(EnumAttribute.Armor));
  }

  public void AddExperience(int amount)
  {
    Experience += amount;
    while (Experience >= GetExperienceForNextLevel())
    {
      Experience -= GetExperienceForNextLevel();
      Level++;
    }
  }

  public int GetExperienceForNextLevel()
  {
    return Level * 100;
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
