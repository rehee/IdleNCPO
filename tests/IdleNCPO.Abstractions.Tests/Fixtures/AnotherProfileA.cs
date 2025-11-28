using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Tests.Fixtures;

/// <summary>
/// Another test profile implementation A
/// </summary>
public class AnotherProfileA : IIdleProfile<EnumAnotherProfile>
{
  public EnumAnotherProfile Key => EnumAnotherProfile.AnotherA;
  public string Name => "Another A";
  public string Description => "Another profile A";
}
