using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Components;

/// <summary>
/// Abstract base class for all component types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public abstract class BaseComponent<T> : IComponent<T> where T : Enum
{
  public abstract T ProfileKey { get; protected set; }
}
