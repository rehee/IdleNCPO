using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Tests.Fixtures;

/// <summary>
/// Test profile implementation B
/// </summary>
public class TestProfileB : IIdleProfile<EnumTestProfile>
{
  public EnumTestProfile Key => EnumTestProfile.TestB;
  public string Name => "Test B";
  public string Description => "Test profile B";
}
