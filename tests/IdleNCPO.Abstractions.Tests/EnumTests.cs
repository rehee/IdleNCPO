using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Tests;

public class EnumTests
{
  [Fact]
  public void EnumMap_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumMap);
    Assert.Equal(EnumMap.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumMonster_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumMonster);
    Assert.Equal(EnumMonster.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumSkill_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumSkill);
    Assert.Equal(EnumSkill.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumItem_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumItem);
    Assert.Equal(EnumItem.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumItemCategory_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumItemCategory);
    Assert.Equal(EnumItemCategory.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumAttribute_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumAttribute);
    Assert.Equal(EnumAttribute.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumDamageType_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumDamageType);
    Assert.Equal(EnumDamageType.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumEquipmentSlot_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumEquipmentSlot);
    Assert.Equal(EnumEquipmentSlot.NotSpecified, defaultValue);
  }

  [Fact]
  public void EnumSupportSkill_ShouldHaveNotSpecifiedAsDefault()
  {
    var defaultValue = default(EnumSupportSkill);
    Assert.Equal(EnumSupportSkill.NotSpecified, defaultValue);
  }
}
