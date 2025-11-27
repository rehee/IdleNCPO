using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Profile for equipment types
/// </summary>
public class EquipmentProfile : BaseProfile<EnumEquipment>
{
  public override EnumEquipment Key { get; }
  public override string Name { get; }
  public override string Description { get; }
  public EnumEquipmentSlot Slot { get; }
  public Dictionary<EnumAttribute, int> BaseAttributes { get; }
  public int RequiredLevel { get; }

  public EquipmentProfile(
    EnumEquipment key,
    string name,
    string description,
    EnumEquipmentSlot slot,
    Dictionary<EnumAttribute, int>? baseAttributes = null,
    int requiredLevel = 1)
  {
    Key = key;
    Name = name;
    Description = description;
    Slot = slot;
    BaseAttributes = baseAttributes ?? new Dictionary<EnumAttribute, int>();
    RequiredLevel = requiredLevel;
  }
}
