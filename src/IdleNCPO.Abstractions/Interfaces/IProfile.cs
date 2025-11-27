namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Base interface for IdleProfile types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public interface IIdleProfile<T> where T : Enum
{
  T Key { get; }
  string Name { get; }
  string Description { get; }
}
