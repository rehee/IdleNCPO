using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for monster runtime data
/// </summary>
public class MonsterIdleComponent : IdleComponent<EnumMonster>, IActor
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
  
  public int X { get; set; }
  public int Y { get; set; }

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
}
