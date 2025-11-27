using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Services;

namespace IdleNCPO.Core.Tests;

public class ProfileServiceTests
{
  private readonly ProfileService _profileService;

  public ProfileServiceTests()
  {
    _profileService = new ProfileService();
  }

  [Fact]
  public void GetMapProfile_StarterVillage_ReturnsProfile()
  {
    var profile = _profileService.GetMapProfile(EnumMap.StarterVillage);
    
    Assert.NotNull(profile);
    Assert.Equal(EnumMap.StarterVillage, profile.Key);
    Assert.Equal("新手村", profile.Name);
  }

  [Fact]
  public void GetMapProfile_NotSpecified_ReturnsNull()
  {
    var profile = _profileService.GetMapProfile(EnumMap.NotSpecified);
    Assert.Null(profile);
  }

  [Fact]
  public void GetMonsterProfile_Skeleton_ReturnsProfile()
  {
    var profile = _profileService.GetMonsterProfile(EnumMonster.Skeleton);
    
    Assert.NotNull(profile);
    Assert.Equal(EnumMonster.Skeleton, profile.Key);
    Assert.Equal("骷髅", profile.Name);
    Assert.True(profile.BaseHealth > 0);
  }

  [Fact]
  public void GetSkillProfile_Fireball_ReturnsProfile()
  {
    var profile = _profileService.GetSkillProfile(EnumSkill.Fireball);
    
    Assert.NotNull(profile);
    Assert.Equal(EnumSkill.Fireball, profile.Key);
    Assert.Equal("火球术", profile.Name);
    Assert.Equal(EnumDamageType.Fire, profile.DamageType);
    Assert.True(profile.IsAreaOfEffect);
  }

  [Fact]
  public void GetEquipmentProfile_LongSword_ReturnsProfile()
  {
    var profile = _profileService.GetEquipmentProfile(EnumItem.LongSword);
    
    Assert.NotNull(profile);
    Assert.Equal(EnumItem.LongSword, profile.Key);
    Assert.Equal("长剑", profile.Name);
    Assert.Equal(EnumEquipmentSlot.MainHand, profile.Slot);
  }

  [Fact]
  public void GetAllMapProfiles_ReturnsMultipleProfiles()
  {
    var profiles = _profileService.GetAllMapProfiles().ToList();
    Assert.NotEmpty(profiles);
    Assert.True(profiles.Count >= 2);
  }

  [Fact]
  public void GetAllMonsterProfiles_ReturnsMultipleProfiles()
  {
    var profiles = _profileService.GetAllMonsterProfiles().ToList();
    Assert.NotEmpty(profiles);
    Assert.True(profiles.Count >= 5);
  }
}
