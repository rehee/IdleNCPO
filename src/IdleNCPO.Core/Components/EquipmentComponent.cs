using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Components;

/// <summary>
/// Component for equipment runtime data
/// </summary>
public class EquipmentComponent : BaseComponent<EnumEquipment>
{
  public override EnumEquipment ProfileKey { get; protected set; }
  
  public Guid Id { get; set; }
  public EnumEquipmentSlot Slot { get; set; }
  public int ItemLevel { get; set; }
  public Dictionary<EnumAttribute, int> Attributes { get; set; } = new();

  public EquipmentComponent(EnumEquipment profileKey)
  {
    ProfileKey = profileKey;
    Id = Guid.NewGuid();
  }

  public int GetAttributeValue(EnumAttribute attribute)
  {
    return Attributes.TryGetValue(attribute, out var value) ? value : 0;
  }
}
