using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Containers;

/// <summary>
/// Container that holds IdleProfile instances of a specific key type
/// </summary>
/// <typeparam name="TKey">The enum type used as key for profiles in this container</typeparam>
public class IdleProfileContainer<TKey> : IIdleProfileContainer<TKey> where TKey : Enum
{
  private readonly Dictionary<TKey, IIdleProfile<TKey>> _profiles = new();

  /// <inheritdoc />
  public int Count => _profiles.Count;

  /// <inheritdoc />
  public void Add(IIdleProfile<TKey> profile)
  {
    ArgumentNullException.ThrowIfNull(profile);
    _profiles[profile.Key] = profile;
  }

  /// <inheritdoc />
  public IIdleProfile<TKey>? Get(TKey key)
  {
    return _profiles.TryGetValue(key, out var profile) ? profile : null;
  }

  /// <inheritdoc />
  public IEnumerable<IIdleProfile<TKey>> GetAll()
  {
    return _profiles.Values;
  }

  /// <inheritdoc />
  public bool Contains(TKey key)
  {
    return _profiles.ContainsKey(key);
  }
}
