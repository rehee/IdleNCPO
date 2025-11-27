namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Base interface for profile types
/// </summary>
/// <typeparam name="T">Enum type for profile key</typeparam>
public interface IProfile<T> where T : Enum
{
  T Key { get; }
  string Name { get; }
  string Description { get; }
}
