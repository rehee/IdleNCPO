using IdleNCPO.Abstractions.Entities;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Entity for storing skill information
/// </summary>
public class SkillEntity : BaseEntity
{
  public EnumSkill SkillType { get; set; }
  public Guid OwnerId { get; set; }
  public int Level { get; set; } = 1;
  public List<EnumSupportSkill> LinkedSupports { get; set; } = new();
}
