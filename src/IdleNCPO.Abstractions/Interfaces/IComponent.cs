namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Base interface for IdleComponent types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public interface IIdleComponent<T> where T : Enum
{
  T ProfileKey { get; }
}
