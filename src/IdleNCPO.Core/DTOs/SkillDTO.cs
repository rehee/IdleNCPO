using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.DTOs;

/// <summary>
/// Data transfer object for skill information
/// </summary>
public class SkillDTO
{
  public Guid Id { get; set; }
  public EnumSkill SkillType { get; set; }
  public int Level { get; set; }
  public List<EnumSupportSkill> LinkedSupports { get; set; } = new();
}
