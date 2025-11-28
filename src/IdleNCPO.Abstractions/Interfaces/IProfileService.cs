using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for profile data access service
/// </summary>
public interface IProfileService
{
  /// <summary>
  /// Get map profile by key
  /// </summary>
  IMapProfile? GetMapProfile(EnumMap key);

  /// <summary>
  /// Get monster profile by key
  /// </summary>
  IMonsterProfile? GetMonsterProfile(EnumMonster key);

  /// <summary>
  /// Get skill profile by key
  /// </summary>
  ISkillProfile? GetSkillProfile(EnumSkill key);

  /// <summary>
  /// Get item profile by key
  /// </summary>
  IItemProfile? GetItemProfile(EnumItem key);

  /// <summary>
  /// Get equipment profile by key
  /// </summary>
  IEquipmentProfile? GetEquipmentProfile(EnumItem key);

  /// <summary>
  /// Get all map profiles
  /// </summary>
  IEnumerable<IMapProfile> GetAllMapProfiles();

  /// <summary>
  /// Get all monster profiles
  /// </summary>
  IEnumerable<IMonsterProfile> GetAllMonsterProfiles();

  /// <summary>
  /// Get all skill profiles
  /// </summary>
  IEnumerable<ISkillProfile> GetAllSkillProfiles();

  /// <summary>
  /// Get all item profiles
  /// </summary>
  IEnumerable<IItemProfile> GetAllItemProfiles();

  /// <summary>
  /// Get all equipment profiles
  /// </summary>
  IEnumerable<IEquipmentProfile> GetAllEquipmentProfiles();
}
