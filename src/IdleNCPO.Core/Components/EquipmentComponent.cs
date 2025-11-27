using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for item runtime data
/// </summary>
public class ItemIdleComponent : IdleComponent<EnumItem>
{
  public override EnumItem ProfileKey { get; protected set; }
  
  public Guid Id { get; set; }
  public EnumItemCategory Category { get; set; }
  public EnumEquipmentSlot? Slot { get; set; }
  public int ItemLevel { get; set; }
  public Dictionary<EnumAttribute, int> Attributes { get; set; } = new();

  public ItemIdleComponent(EnumItem profileKey)
  {
    ProfileKey = profileKey;
    Id = Guid.NewGuid();
  }

  public int GetAttributeValue(EnumAttribute attribute)
  {
    return Attributes.TryGetValue(attribute, out var value) ? value : 0;
  }

  public bool IsEquipment => Category == EnumItemCategory.Equipment;
}
