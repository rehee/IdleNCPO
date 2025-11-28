using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for skill profile
/// </summary>
public interface ISkillProfile : IIdleProfile<EnumSkill>
{
  int BaseDamage { get; }
  int ManaCost { get; }
  int Cooldown { get; }
  int Range { get; }
  EnumDamageType DamageType { get; }
  bool IsAreaOfEffect { get; }
  int AreaRadius { get; }
}
