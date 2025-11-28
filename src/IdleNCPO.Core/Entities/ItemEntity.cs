using IdleNCPO.Abstractions.Entities;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Entity for storing item information
/// </summary>
public class ItemEntity : BaseEntity
{
  public EnumItem ItemType { get; set; }
  public EnumItemCategory Category { get; set; }
  public Guid? OwnerId { get; set; }
  public EnumEquipmentSlot? EquippedSlot { get; set; }
  public int ItemLevel { get; set; } = 1;
  public Dictionary<EnumAttribute, int> Attributes { get; set; } = new();
}
