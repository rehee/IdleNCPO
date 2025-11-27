using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for character runtime data
/// </summary>
public class CharacterIdleComponent : IdleComponent<EnumMonster>, IActor
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
  
  public int X { get; set; }
  public int Y { get; set; }
  
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
}
