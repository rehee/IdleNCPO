using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Profiles;

/// <summary>
/// Abstract base class for all IdleProfile types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public abstract class IdleProfile<T> : IIdleProfile<T> where T : Enum
{
  public abstract T Key { get; }
  public abstract string Name { get; }
  public virtual string Description => string.Empty;
}
