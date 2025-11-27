using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Profile for skill types
/// </summary>
public class SkillProfile : BaseProfile<EnumSkill>
{
  public override EnumSkill Key { get; }
  public override string Name { get; }
  public override string Description { get; }
  public int BaseDamage { get; }
  public int ManaCost { get; }
  public int Cooldown { get; }
  public int Range { get; }
  public EnumDamageType DamageType { get; }
  public bool IsAreaOfEffect { get; }
  public int AreaRadius { get; }

  public SkillProfile(
    EnumSkill key,
    string name,
    string description,
    int baseDamage,
    int manaCost,
    int cooldown,
    int range,
    EnumDamageType damageType = EnumDamageType.Physical,
    bool isAreaOfEffect = false,
    int areaRadius = 0)
  {
    Key = key;
    Name = name;
    Description = description;
    BaseDamage = baseDamage;
    ManaCost = manaCost;
    Cooldown = cooldown;
    Range = range;
    DamageType = damageType;
    IsAreaOfEffect = isAreaOfEffect;
    AreaRadius = areaRadius;
  }
}
