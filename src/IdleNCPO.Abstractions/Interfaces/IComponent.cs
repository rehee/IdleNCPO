namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Base interface for component types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public interface IComponent<T> where T : Enum
{
  T ProfileKey { get; }
}
