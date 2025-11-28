using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Components;

/// <summary>
/// Abstract base class for all IdleComponent types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public abstract class IdleComponent<T> : IIdleComponent<T> where T : Enum
{
  public abstract T ProfileKey { get; protected set; }
}
