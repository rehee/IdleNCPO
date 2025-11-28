using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Components;

namespace IdleNCPO.Core.Tests;

public class CharacterIdleComponentTests
{
  [Fact]
  public void CharacterIdleComponent_MaxHealth_ShouldCalculateFromVitality()
  {
    var character = new CharacterIdleComponent
    {
      Vitality = 10
    };

    // MaxHealth = Vitality * 10 + 50 = 10 * 10 + 50 = 150
    Assert.Equal(150, character.MaxHealth);
  }

  [Fact]
  public void CharacterIdleComponent_MaxMana_ShouldCalculateFromIntelligence()
  {
    var character = new CharacterIdleComponent
    {
      Intelligence = 10
    };

    // MaxMana = Intelligence * 5 + 30 = 10 * 5 + 30 = 80
    Assert.Equal(80, character.MaxMana);
  }

  [Fact]
  public void CharacterIdleComponent_TakeDamage_ShouldReduceHealth()
  {
    var character = new CharacterIdleComponent
    {
      Vitality = 10,
      CurrentHealth = 100
    };

    character.TakeDamage(30);

    Assert.Equal(70, character.CurrentHealth);
  }

  [Fact]
  public void CharacterIdleComponent_TakeDamage_ShouldNotGoBelowZero()
  {
    var character = new CharacterIdleComponent
    {
      Vitality = 10,
      CurrentHealth = 50
    };

    character.TakeDamage(100);

    Assert.Equal(0, character.CurrentHealth);
  }

  [Fact]
  public void CharacterIdleComponent_Heal_ShouldIncreaseHealth()
  {
    var character = new CharacterIdleComponent
    {
      Vitality = 10,
      CurrentHealth = 50
    };

    character.Heal(30);

    Assert.Equal(80, character.CurrentHealth);
  }

  [Fact]
  public void CharacterIdleComponent_Heal_ShouldNotExceedMaxHealth()
  {
    var character = new CharacterIdleComponent
    {
      Vitality = 10,
      CurrentHealth = 140
    };

    character.Heal(50);

    Assert.Equal(character.MaxHealth, character.CurrentHealth);
  }

  [Fact]
  public void CharacterIdleComponent_IsAlive_ShouldBeTrueWhenHealthPositive()
  {
    var character = new CharacterIdleComponent
    {
      CurrentHealth = 1
    };

    Assert.True(character.IsAlive);
  }

  [Fact]
  public void CharacterIdleComponent_IsAlive_ShouldBeFalseWhenHealthZero()
  {
    var character = new CharacterIdleComponent
    {
      CurrentHealth = 0
    };

    Assert.False(character.IsAlive);
  }

  [Fact]
  public void CharacterIdleComponent_AddExperience_ShouldLevelUp()
  {
    var character = new CharacterIdleComponent
    {
      Level = 1,
      Experience = 0
    };

    // Level 1 needs 100 exp to level up
    character.AddExperience(150);

    Assert.Equal(2, character.Level);
    Assert.Equal(50, character.Experience);
  }

  [Fact]
  public void CharacterIdleComponent_GetArmor_ShouldSumEquipmentArmor()
  {
    var character = new CharacterIdleComponent();
    character.Equipment.Add(new ItemIdleComponent(EnumItem.Shield)
    {
      Category = EnumItemCategory.Equipment,
      Attributes = new Dictionary<EnumAttribute, int>
      {
        { EnumAttribute.Armor, 10 }
      }
    });
    character.Equipment.Add(new ItemIdleComponent(EnumItem.ChestArmor)
    {
      Category = EnumItemCategory.Equipment,
      Attributes = new Dictionary<EnumAttribute, int>
      {
        { EnumAttribute.Armor, 15 }
      }
    });

    Assert.Equal(25, character.GetArmor());
  }
}
