using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Abstractions.Profiles;

namespace IdleNCPO.Core.Profiles;

/// <summary>
/// Abstract base profile for all item types
/// </summary>
public abstract class ItemIdleProfile : IdleProfile<EnumItem>, IItemProfile
{
  public abstract EnumItemCategory Category { get; }
  public virtual int SellValue => 1;
}

/// <summary>
/// Abstract base profile for equipment items
/// </summary>
public abstract class EquipmentIdleProfile : ItemIdleProfile, IEquipmentProfile
{
  public override EnumItemCategory Category => EnumItemCategory.Equipment;
  public abstract EnumEquipmentCategory EquipmentCategory { get; }
  public abstract EnumEquipmentSlot Slot { get; }
  public abstract Dictionary<EnumAttribute, int> BaseAttributes { get; }
  public virtual int RequiredLevel => 1;
}

/// <summary>
/// Abstract base profile for weapon items
/// </summary>
public abstract class WeaponIdleProfile : EquipmentIdleProfile
{
  public override EnumEquipmentCategory EquipmentCategory => EnumEquipmentCategory.Weapon;
}

/// <summary>
/// Abstract base profile for armor items
/// </summary>
public abstract class ArmorIdleProfile : EquipmentIdleProfile
{
  public override EnumEquipmentCategory EquipmentCategory => EnumEquipmentCategory.Armor;
}

/// <summary>
/// Abstract base profile for accessory items
/// </summary>
public abstract class AccessoryIdleProfile : EquipmentIdleProfile
{
  public override EnumEquipmentCategory EquipmentCategory => EnumEquipmentCategory.Accessory;
}

/// <summary>
/// Abstract base profile for junk items
/// </summary>
public abstract class JunkIdleProfile : ItemIdleProfile
{
  public override EnumItemCategory Category => EnumItemCategory.Junk;
}

// Weapon implementations
public class LongSwordItemProfile : WeaponIdleProfile
{
  public override EnumItem Key => EnumItem.LongSword;
  public override string Name => "长剑";
  public override string Description => "标准的单手剑";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.MainHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Strength, 5 } };
  public override int SellValue => 10;
}

public class ShortSwordItemProfile : WeaponIdleProfile
{
  public override EnumItem Key => EnumItem.ShortSword;
  public override string Name => "短剑";
  public override string Description => "轻便的短剑";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.MainHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Dexterity, 3 }, { EnumAttribute.Strength, 2 } };
  public override int SellValue => 8;
}

public class DaggerItemProfile : WeaponIdleProfile
{
  public override EnumItem Key => EnumItem.Dagger;
  public override string Name => "匕首";
  public override string Description => "快速的近战武器";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.MainHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Dexterity, 5 } };
  public override int SellValue => 7;
}

public class StaffItemProfile : WeaponIdleProfile
{
  public override EnumItem Key => EnumItem.Staff;
  public override string Name => "法杖";
  public override string Description => "施法者的武器";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.MainHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Intelligence, 8 } };
  public override int SellValue => 12;
}

public class BowItemProfile : WeaponIdleProfile
{
  public override EnumItem Key => EnumItem.Bow;
  public override string Name => "弓";
  public override string Description => "远程武器";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.MainHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Dexterity, 6 } };
  public override int SellValue => 10;
}

// Armor implementations
public class ShieldItemProfile : ArmorIdleProfile
{
  public override EnumItem Key => EnumItem.Shield;
  public override string Name => "盾牌";
  public override string Description => "提供防护的盾牌";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.OffHand;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Armor, 10 } };
  public override int SellValue => 8;
}

public class HelmetItemProfile : ArmorIdleProfile
{
  public override EnumItem Key => EnumItem.Helmet;
  public override string Name => "头盔";
  public override string Description => "保护头部的护甲";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Head;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Armor, 5 } };
  public override int SellValue => 6;
}

public class ChestArmorItemProfile : ArmorIdleProfile
{
  public override EnumItem Key => EnumItem.ChestArmor;
  public override string Name => "胸甲";
  public override string Description => "保护躯干的护甲";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Body;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Armor, 15 }, { EnumAttribute.Vitality, 3 } };
  public override int SellValue => 15;
}

public class BootsItemProfile : ArmorIdleProfile
{
  public override EnumItem Key => EnumItem.Boots;
  public override string Name => "靴子";
  public override string Description => "保护脚部的护甲";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Feet;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Armor, 3 }, { EnumAttribute.Dexterity, 2 } };
  public override int SellValue => 5;
}

public class GlovesItemProfile : ArmorIdleProfile
{
  public override EnumItem Key => EnumItem.Gloves;
  public override string Name => "手套";
  public override string Description => "保护双手的护甲";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Hands;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Armor, 2 }, { EnumAttribute.Strength, 1 } };
  public override int SellValue => 4;
}

// Accessory implementations
public class RingItemProfile : AccessoryIdleProfile
{
  public override EnumItem Key => EnumItem.Ring;
  public override string Name => "戒指";
  public override string Description => "提供魔法加成的戒指";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Ring1;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Intelligence, 2 } };
  public override int SellValue => 20;
}

public class AmuletItemProfile : AccessoryIdleProfile
{
  public override EnumItem Key => EnumItem.Amulet;
  public override string Name => "护身符";
  public override string Description => "提供保护的护身符";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Amulet;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Vitality, 3 } };
  public override int SellValue => 25;
}

public class BeltItemProfile : AccessoryIdleProfile
{
  public override EnumItem Key => EnumItem.Belt;
  public override string Name => "腰带";
  public override string Description => "提供额外存储的腰带";
  public override EnumEquipmentSlot Slot => EnumEquipmentSlot.Belt;
  public override Dictionary<EnumAttribute, int> BaseAttributes => new() { { EnumAttribute.Vitality, 2 } };
  public override int SellValue => 10;
}

// Junk implementations
public class BrokenSwordJunkProfile : JunkIdleProfile
{
  public override EnumItem Key => EnumItem.BrokenSword;
  public override string Name => "破碎的剑";
  public override string Description => "已经无法使用的武器残骸";
  public override int SellValue => 2;
}

public class RustyHelmetJunkProfile : JunkIdleProfile
{
  public override EnumItem Key => EnumItem.RustyHelmet;
  public override string Name => "生锈的头盔";
  public override string Description => "已经锈迹斑斑的头盔";
  public override int SellValue => 1;
}

public class MonsterBoneJunkProfile : JunkIdleProfile
{
  public override EnumItem Key => EnumItem.MonsterBone;
  public override string Name => "怪物骨头";
  public override string Description => "从怪物身上掉落的骨头";
  public override int SellValue => 3;
}
