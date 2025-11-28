using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Tests.Fixtures;

/// <summary>
/// Test profile implementation A
/// </summary>
public class TestProfileA : IIdleProfile<EnumTestProfile>
{
  public EnumTestProfile Key => EnumTestProfile.TestA;
  public string Name => "Test A";
  public string Description => "Test profile A";
}
