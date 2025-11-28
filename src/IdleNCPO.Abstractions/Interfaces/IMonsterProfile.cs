using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for monster profile
/// </summary>
public interface IMonsterProfile : IIdleProfile<EnumMonster>
{
  int BaseHealth { get; }
  int BaseDamage { get; }
  int BaseArmor { get; }
  int BaseExperience { get; }
  EnumDamageType DamageType { get; }
}
