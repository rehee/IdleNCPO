using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Abstract base profile for all skill types
/// </summary>
public abstract class SkillIdleProfile : IdleProfile<EnumSkill>, ISkillProfile
{
  public abstract int BaseDamage { get; }
  public abstract int ManaCost { get; }
  public abstract int Cooldown { get; }
  public abstract int Range { get; }
  public virtual EnumDamageType DamageType => EnumDamageType.Physical;
  public virtual bool IsAreaOfEffect => false;
  public virtual int AreaRadius => 0;
}

/// <summary>
/// Profile for Basic Attack skill
/// </summary>
public class BasicAttackSkillProfile : SkillIdleProfile
{
  public override EnumSkill Key => EnumSkill.BasicAttack;
  public override string Name => "普通攻击";
  public override string Description => "基础物理攻击";
  public override int BaseDamage => 10;
  public override int ManaCost => 0;
  public override int Cooldown => 1;
  public override int Range => 1;
}

/// <summary>
/// Profile for Fireball skill
/// </summary>
public class FireballSkillProfile : SkillIdleProfile
{
  public override EnumSkill Key => EnumSkill.Fireball;
  public override string Name => "火球术";
  public override string Description => "发射一团火焰";
  public override int BaseDamage => 25;
  public override int ManaCost => 15;
  public override int Cooldown => 3;
  public override int Range => 5;
  public override EnumDamageType DamageType => EnumDamageType.Fire;
  public override bool IsAreaOfEffect => true;
  public override int AreaRadius => 2;
}

/// <summary>
/// Profile for Ice Arrow skill
/// </summary>
public class IceArrowSkillProfile : SkillIdleProfile
{
  public override EnumSkill Key => EnumSkill.IceArrow;
  public override string Name => "冰箭";
  public override string Description => "发射冰冷的箭矢";
  public override int BaseDamage => 20;
  public override int ManaCost => 10;
  public override int Cooldown => 2;
  public override int Range => 6;
  public override EnumDamageType DamageType => EnumDamageType.Cold;
}

/// <summary>
/// Profile for Lightning Bolt skill
/// </summary>
public class LightningBoltSkillProfile : SkillIdleProfile
{
  public override EnumSkill Key => EnumSkill.LightningBolt;
  public override string Name => "闪电";
  public override string Description => "召唤闪电打击敌人";
  public override int BaseDamage => 30;
  public override int ManaCost => 20;
  public override int Cooldown => 4;
  public override int Range => 7;
  public override EnumDamageType DamageType => EnumDamageType.Lightning;
}

/// <summary>
/// Profile for Healing Touch skill
/// </summary>
public class HealingTouchSkillProfile : SkillIdleProfile
{
  public override EnumSkill Key => EnumSkill.HealingTouch;
  public override string Name => "治疗之触";
  public override string Description => "恢复生命值";
  public override int BaseDamage => -30; // Negative means healing
  public override int ManaCost => 25;
  public override int Cooldown => 5;
  public override int Range => 1;
}
