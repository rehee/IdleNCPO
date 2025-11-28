using IdleNCPO.Abstractions.Containers;
using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Abstractions.Tests.Fixtures;

namespace IdleNCPO.Abstractions.Tests;

public class IdleProfilePoolTests
{
  public IdleProfilePoolTests()
  {
    // Reset pool before each test
    IdleProfilePool.Reset();
  }

  [Fact]
  public void Pool_Initialize_ShouldSetInitializedFlag()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    Assert.True(IdleProfilePool.IsInitialized);
  }

  [Fact]
  public void Pool_Initialize_SecondCall_ShouldHaveNoEffect()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);
    var firstKeyTypes = IdleProfilePool.GetRegisteredKeyTypes().ToList();

    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);
    var secondKeyTypes = IdleProfilePool.GetRegisteredKeyTypes().ToList();

    Assert.Equal(firstKeyTypes.Count, secondKeyTypes.Count);
  }

  [Fact]
  public void Pool_Get_ShouldReturnProfile()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var profile = IdleProfilePool.Get(EnumTestProfile.TestA);

    Assert.NotNull(profile);
    Assert.Equal(EnumTestProfile.TestA, profile.Key);
    Assert.Equal("Test A", profile.Name);
  }

  [Fact]
  public void Pool_Get_NotFound_ShouldReturnNull()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var profile = IdleProfilePool.Get(EnumTestProfile.TestC);

    Assert.Null(profile);
  }

  [Fact]
  public void Pool_GetByTypeAndInt_ShouldReturnProfile()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var profile = IdleProfilePool.Get(typeof(EnumTestProfile), 1);

    Assert.NotNull(profile);
    var typedProfile = Assert.IsAssignableFrom<IIdleProfile<EnumTestProfile>>(profile);
    Assert.Equal(EnumTestProfile.TestA, typedProfile.Key);
  }

  [Fact]
  public void Pool_GetByTypeAndInt_InvalidType_ShouldThrow()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    Assert.Throws<ArgumentException>(() => IdleProfilePool.Get(typeof(string), 1));
  }

  [Fact]
  public void Pool_GetAll_ShouldReturnAllProfilesOfType()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var profiles = IdleProfilePool.GetAll<EnumTestProfile>().ToList();

    Assert.Equal(2, profiles.Count);
    Assert.Contains(profiles, p => p.Key == EnumTestProfile.TestA);
    Assert.Contains(profiles, p => p.Key == EnumTestProfile.TestB);
  }

  [Fact]
  public void Pool_GetAllByType_ShouldReturnAllProfilesOfType()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var profiles = IdleProfilePool.GetAll(typeof(EnumTestProfile)).ToList();

    Assert.Equal(2, profiles.Count);
  }

  [Fact]
  public void Pool_GetAllByType_InvalidType_ShouldThrow()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    Assert.Throws<ArgumentException>(() => IdleProfilePool.GetAll(typeof(string)).ToList());
  }

  [Fact]
  public void Pool_GetContainer_ShouldReturnContainer()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var container = IdleProfilePool.GetContainer<EnumTestProfile>();

    Assert.NotNull(container);
    Assert.Equal(2, container.Count);
  }

  [Fact]
  public void Pool_ShouldGroupProfilesByKeyType()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var testProfiles = IdleProfilePool.GetAll<EnumTestProfile>().ToList();
    var anotherProfiles = IdleProfilePool.GetAll<EnumAnotherProfile>().ToList();

    Assert.Equal(2, testProfiles.Count);
    Assert.Single(anotherProfiles);
  }

  [Fact]
  public void Pool_GetRegisteredKeyTypes_ShouldReturnAllTypes()
  {
    IdleProfilePool.Initialize(typeof(IdleProfilePoolTests).Assembly);

    var keyTypes = IdleProfilePool.GetRegisteredKeyTypes().ToList();

    Assert.Contains(typeof(EnumTestProfile), keyTypes);
    Assert.Contains(typeof(EnumAnotherProfile), keyTypes);
  }
}
