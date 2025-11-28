using IdleNCPO.Abstractions.Containers;
using IdleNCPO.Abstractions.Tests.Fixtures;

namespace IdleNCPO.Abstractions.Tests;

public class IdleProfileContainerTests
{
  [Fact]
  public void Container_Add_ShouldIncreaseCount()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();
    var profile = new TestProfileA();

    container.Add(profile);

    Assert.Equal(1, container.Count);
  }

  [Fact]
  public void Container_Add_ShouldThrowOnNull()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();

    Assert.Throws<ArgumentNullException>(() => container.Add(null!));
  }

  [Fact]
  public void Container_Add_SameKey_ShouldReplace()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();
    var profile1 = new TestProfileA();
    var profile2 = new TestProfileA();

    container.Add(profile1);
    container.Add(profile2);

    Assert.Equal(1, container.Count);
    Assert.Same(profile2, container.Get(EnumTestProfile.TestA));
  }

  [Fact]
  public void Container_Get_ShouldReturnProfile()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();
    var profile = new TestProfileA();
    container.Add(profile);

    var result = container.Get(EnumTestProfile.TestA);

    Assert.Same(profile, result);
  }

  [Fact]
  public void Container_Get_NotFound_ShouldReturnNull()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();

    var result = container.Get(EnumTestProfile.TestA);

    Assert.Null(result);
  }

  [Fact]
  public void Container_GetAll_ShouldReturnAllProfiles()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();
    var profileA = new TestProfileA();
    var profileB = new TestProfileB();
    container.Add(profileA);
    container.Add(profileB);

    var results = container.GetAll().ToList();

    Assert.Equal(2, results.Count);
    Assert.Contains(profileA, results);
    Assert.Contains(profileB, results);
  }

  [Fact]
  public void Container_Contains_ShouldReturnTrueWhenExists()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();
    var profile = new TestProfileA();
    container.Add(profile);

    Assert.True(container.Contains(EnumTestProfile.TestA));
  }

  [Fact]
  public void Container_Contains_ShouldReturnFalseWhenNotExists()
  {
    var container = new IdleProfileContainer<EnumTestProfile>();

    Assert.False(container.Contains(EnumTestProfile.TestA));
  }
}
