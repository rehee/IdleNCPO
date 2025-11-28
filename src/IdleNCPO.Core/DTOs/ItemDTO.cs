using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.DTOs;

/// <summary>
/// Data transfer object for item information
/// </summary>
public class ItemDTO
{
  public Guid Id { get; set; }
  public EnumItem ItemType { get; set; }
  public EnumItemCategory Category { get; set; }
  public EnumEquipmentSlot? EquippedSlot { get; set; }
  public int ItemLevel { get; set; }
  public Dictionary<EnumAttribute, int> Attributes { get; set; } = new();
}
