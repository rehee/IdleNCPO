using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Containers;

/// <summary>
/// Interface for a container that holds IdleProfile instances of a specific key type
/// </summary>
/// <typeparam name="TKey">The enum type used as key for profiles in this container</typeparam>
public interface IIdleProfileContainer<TKey> where TKey : Enum
{
  /// <summary>
  /// Add a profile to the container
  /// </summary>
  /// <param name="profile">The profile to add</param>
  void Add(IIdleProfile<TKey> profile);

  /// <summary>
  /// Get a profile by its key
  /// </summary>
  /// <param name="key">The key of the profile</param>
  /// <returns>The profile if found, null otherwise</returns>
  IIdleProfile<TKey>? Get(TKey key);

  /// <summary>
  /// Get all profiles in the container
  /// </summary>
  /// <returns>All profiles as an enumerable</returns>
  IEnumerable<IIdleProfile<TKey>> GetAll();

  /// <summary>
  /// Check if a profile with the given key exists
  /// </summary>
  /// <param name="key">The key to check</param>
  /// <returns>True if exists, false otherwise</returns>
  bool Contains(TKey key);

  /// <summary>
  /// Get the count of profiles in the container
  /// </summary>
  int Count { get; }
}
