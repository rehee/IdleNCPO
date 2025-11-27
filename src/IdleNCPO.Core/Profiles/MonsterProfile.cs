using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Profile for monster types
/// </summary>
public class MonsterProfile : BaseProfile<EnumMonster>
{
  public override EnumMonster Key { get; }
  public override string Name { get; }
  public override string Description { get; }
  public int BaseHealth { get; }
  public int BaseDamage { get; }
  public int BaseArmor { get; }
  public int BaseExperience { get; }
  public EnumDamageType DamageType { get; }

  public MonsterProfile(
    EnumMonster key,
    string name,
    string description,
    int baseHealth,
    int baseDamage,
    int baseArmor,
    int baseExperience,
    EnumDamageType damageType = EnumDamageType.Physical)
  {
    Key = key;
    Name = name;
    Description = description;
    BaseHealth = baseHealth;
    BaseDamage = baseDamage;
    BaseArmor = baseArmor;
    BaseExperience = baseExperience;
    DamageType = damageType;
  }
}
