using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for item profile
/// </summary>
public interface IItemProfile : IIdleProfile<EnumItem>
{
  EnumItemCategory Category { get; }
  int SellValue { get; }
}

/// <summary>
/// Interface for equipment profile
/// </summary>
public interface IEquipmentProfile : IItemProfile
{
  EnumEquipmentCategory EquipmentCategory { get; }
  EnumEquipmentSlot Slot { get; }
  Dictionary<EnumAttribute, int> BaseAttributes { get; }
  int RequiredLevel { get; }
}
