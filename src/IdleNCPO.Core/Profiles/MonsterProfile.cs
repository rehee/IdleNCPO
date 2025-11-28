using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Abstract base profile for all monster types
/// </summary>
public abstract class MonsterIdleProfile : IdleProfile<EnumMonster>
{
  public abstract int BaseHealth { get; }
  public abstract int BaseDamage { get; }
  public abstract int BaseArmor { get; }
  public abstract int BaseExperience { get; }
  public virtual EnumDamageType DamageType => EnumDamageType.Physical;
}

/// <summary>
/// Profile for Skeleton monster
/// </summary>
public class SkeletonMonsterProfile : MonsterIdleProfile
{
  public override EnumMonster Key => EnumMonster.Skeleton;
  public override string Name => "骷髅";
  public override string Description => "基础的亡灵生物";
  public override int BaseHealth => 50;
  public override int BaseDamage => 10;
  public override int BaseArmor => 2;
  public override int BaseExperience => 20;
}

/// <summary>
/// Profile for Zombie monster
/// </summary>
public class ZombieMonsterProfile : MonsterIdleProfile
{
  public override EnumMonster Key => EnumMonster.Zombie;
  public override string Name => "僵尸";
  public override string Description => "缓慢但强壮的亡灵";
  public override int BaseHealth => 80;
  public override int BaseDamage => 15;
  public override int BaseArmor => 5;
  public override int BaseExperience => 30;
}

/// <summary>
/// Profile for Goblin monster
/// </summary>
public class GoblinMonsterProfile : MonsterIdleProfile
{
  public override EnumMonster Key => EnumMonster.Goblin;
  public override string Name => "哥布林";
  public override string Description => "敏捷的小型怪物";
  public override int BaseHealth => 40;
  public override int BaseDamage => 12;
  public override int BaseArmor => 1;
  public override int BaseExperience => 25;
}

/// <summary>
/// Profile for Wolf monster
/// </summary>
public class WolfMonsterProfile : MonsterIdleProfile
{
  public override EnumMonster Key => EnumMonster.Wolf;
  public override string Name => "野狼";
  public override string Description => "快速的野兽";
  public override int BaseHealth => 45;
  public override int BaseDamage => 14;
  public override int BaseArmor => 2;
  public override int BaseExperience => 22;
}

/// <summary>
/// Profile for Spider monster
/// </summary>
public class SpiderMonsterProfile : MonsterIdleProfile
{
  public override EnumMonster Key => EnumMonster.Spider;
  public override string Name => "巨型蜘蛛";
  public override string Description => "有毒的节肢动物";
  public override int BaseHealth => 35;
  public override int BaseDamage => 18;
  public override int BaseArmor => 1;
  public override int BaseExperience => 28;
  public override EnumDamageType DamageType => EnumDamageType.Chaos;
}
