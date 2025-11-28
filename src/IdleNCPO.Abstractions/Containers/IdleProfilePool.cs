using System.Reflection;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Containers;

/// <summary>
/// Static pool that holds all IdleProfile containers
/// Automatically discovers and registers profiles from assemblies at initialization
/// </summary>
public static class IdleProfilePool
{
  private static readonly object _lock = new();
  private static bool _initialized;
  private static readonly Dictionary<Type, object> _containers = new();

  /// <summary>
  /// Check if the pool has been initialized
  /// </summary>
  public static bool IsInitialized => _initialized;

  /// <summary>
  /// Initialize the profile pool by scanning assemblies for IdleProfile implementations
  /// This method can only be run once; subsequent calls have no effect
  /// </summary>
  /// <param name="assemblies">Assemblies to scan for profiles. If null or empty, scans all loaded assemblies</param>
  public static void Initialize(params Assembly[]? assemblies)
  {
    lock (_lock)
    {
      if (_initialized) return;

      var assembliesToScan = (assemblies == null || assemblies.Length == 0)
        ? AppDomain.CurrentDomain.GetAssemblies()
        : assemblies;

      ScanAndRegisterProfiles(assembliesToScan);
      _initialized = true;
    }
  }

  /// <summary>
  /// Get a profile by its key
  /// </summary>
  /// <typeparam name="TKey">The enum type of the profile key</typeparam>
  /// <param name="key">The key of the profile</param>
  /// <returns>The profile if found, null otherwise</returns>
  public static IIdleProfile<TKey>? Get<TKey>(TKey key) where TKey : Enum
  {
    var container = GetContainer<TKey>();
    return container?.Get(key);
  }

  /// <summary>
  /// Get a profile by key type and int value
  /// </summary>
  /// <param name="keyType">The type of the enum key</param>
  /// <param name="keyValue">The int value of the key</param>
  /// <returns>The profile if found, null otherwise</returns>
  public static object? Get(Type keyType, int keyValue)
  {
    if (!keyType.IsEnum)
      throw new ArgumentException("keyType must be an enum type", nameof(keyType));

    if (!_containers.TryGetValue(keyType, out var containerObj))
      return null;

    // Use reflection to call Get method on the container
    var containerType = containerObj.GetType();
    var getMethod = containerType.GetMethod("Get");
    if (getMethod == null) return null;

    var enumValue = Enum.ToObject(keyType, keyValue);
    return getMethod.Invoke(containerObj, new[] { enumValue });
  }

  /// <summary>
  /// Get all profiles of a specific key type
  /// </summary>
  /// <typeparam name="TKey">The enum type of the profile key</typeparam>
  /// <returns>All profiles of the specified type</returns>
  public static IEnumerable<IIdleProfile<TKey>> GetAll<TKey>() where TKey : Enum
  {
    var container = GetContainer<TKey>();
    return container?.GetAll() ?? Enumerable.Empty<IIdleProfile<TKey>>();
  }

  /// <summary>
  /// Get all profiles of a specific key type (non-generic version)
  /// </summary>
  /// <param name="keyType">The type of the enum key</param>
  /// <returns>All profiles of the specified type as objects</returns>
  public static IEnumerable<object> GetAll(Type keyType)
  {
    if (!keyType.IsEnum)
      throw new ArgumentException("keyType must be an enum type", nameof(keyType));

    if (!_containers.TryGetValue(keyType, out var containerObj))
      return Enumerable.Empty<object>();

    // Use reflection to call GetAll method on the container
    var containerType = containerObj.GetType();
    var getAllMethod = containerType.GetMethod("GetAll");
    if (getAllMethod == null) return Enumerable.Empty<object>();

    var result = getAllMethod.Invoke(containerObj, null);
    if (result is System.Collections.IEnumerable enumerable)
    {
      return enumerable.Cast<object>();
    }

    return Enumerable.Empty<object>();
  }

  /// <summary>
  /// Get the container for a specific key type
  /// </summary>
  /// <typeparam name="TKey">The enum type of the profile key</typeparam>
  /// <returns>The container if found, null otherwise</returns>
  public static IIdleProfileContainer<TKey>? GetContainer<TKey>() where TKey : Enum
  {
    return _containers.TryGetValue(typeof(TKey), out var container)
      ? container as IIdleProfileContainer<TKey>
      : null;
  }

  /// <summary>
  /// Get all registered key types
  /// </summary>
  /// <returns>All registered key types</returns>
  public static IEnumerable<Type> GetRegisteredKeyTypes()
  {
    return _containers.Keys;
  }

  /// <summary>
  /// Reset the pool (for testing purposes only)
  /// </summary>
  internal static void Reset()
  {
    lock (_lock)
    {
      _containers.Clear();
      _initialized = false;
    }
  }

  private static void ScanAndRegisterProfiles(IEnumerable<Assembly> assemblies)
  {
    var profileInterface = typeof(IIdleProfile<>);

    foreach (var assembly in assemblies)
    {
      Type[] types;
      try
      {
        types = assembly.GetTypes();
      }
      catch (ReflectionTypeLoadException ex)
      {
        // Some types couldn't be loaded, use the ones that could
        types = ex.Types.Where(t => t != null).ToArray()!;
      }
      catch
      {
        // Skip assemblies that can't be scanned
        continue;
      }

      foreach (var type in types)
      {
        if (type.IsAbstract || type.IsInterface) continue;

        // Find if this type implements IIdleProfile<T>
        var implementedInterfaces = type.GetInterfaces()
          .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == profileInterface)
          .ToList();

        foreach (var iface in implementedInterfaces)
        {
          var keyType = iface.GetGenericArguments()[0];
          RegisterProfile(type, keyType);
        }
      }
    }
  }

  private static void RegisterProfile(Type profileType, Type keyType)
  {
    // Get or create container for this key type
    if (!_containers.TryGetValue(keyType, out var containerObj))
    {
      var containerType = typeof(IdleProfileContainer<>).MakeGenericType(keyType);
      containerObj = Activator.CreateInstance(containerType)!;
      _containers[keyType] = containerObj;
    }

    // Create profile instance
    object? profileInstance;
    try
    {
      profileInstance = Activator.CreateInstance(profileType);
    }
    catch
    {
      // Skip types that can't be instantiated
      return;
    }

    if (profileInstance == null) return;

    // Add to container using reflection
    var addMethod = containerObj.GetType().GetMethod("Add");
    addMethod?.Invoke(containerObj, new[] { profileInstance });
  }
}
