using IdleNCPO.Core.Components;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Core.Entities;

namespace IdleNCPO.Core.Helpers;

/// <summary>
/// Helper class for entity to DTO conversions
/// </summary>
public static class EntityHelper
{
  public static CharacterDTO ToDTO(this ActorEntity entity, List<ItemEntity>? items = null, List<SkillEntity>? skills = null)
  {
    return new CharacterDTO
    {
      Id = entity.Id,
      Name = entity.Name,
      Level = entity.Level,
      Experience = entity.Experience,
      Strength = entity.Strength,
      Dexterity = entity.Dexterity,
      Intelligence = entity.Intelligence,
      Vitality = entity.Vitality,
      Equipment = items?.Select(i => i.ToDTO()).ToList() ?? new List<ItemDTO>(),
      Skills = skills?.Select(s => s.ToDTO()).ToList() ?? new List<SkillDTO>()
    };
  }

  public static ItemDTO ToDTO(this ItemEntity entity)
  {
    return new ItemDTO
    {
      Id = entity.Id,
      EquipmentType = entity.EquipmentType,
      EquippedSlot = entity.EquippedSlot,
      ItemLevel = entity.ItemLevel,
      Attributes = new Dictionary<IdleNCPO.Abstractions.Enums.EnumAttribute, int>(entity.Attributes)
    };
  }

  public static SkillDTO ToDTO(this SkillEntity entity)
  {
    return new SkillDTO
    {
      Id = entity.Id,
      SkillType = entity.SkillType,
      Level = entity.Level,
      LinkedSupports = new List<IdleNCPO.Abstractions.Enums.EnumSupportSkill>(entity.LinkedSupports)
    };
  }
}
