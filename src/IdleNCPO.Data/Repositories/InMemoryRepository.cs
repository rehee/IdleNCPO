using System.Linq.Expressions;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Data.Repositories;

/// <summary>
/// In-memory repository implementation for offline mode
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
  protected readonly List<T> _items = new();
  protected readonly object _lock = new();

  public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      var item = _items.FirstOrDefault(e => e.Id == id);
      return Task.FromResult(item);
    }
  }

  public virtual Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      return Task.FromResult<IEnumerable<T>>(_items.ToList());
    }
  }

  public virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      var compiledPredicate = predicate.Compile();
      var results = _items.Where(compiledPredicate).ToList();
      return Task.FromResult<IEnumerable<T>>(results);
    }
  }

  public virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      entity.CreatedAt = DateTime.UtcNow;
      _items.Add(entity);
      return Task.FromResult(entity);
    }
  }

  public virtual Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      var index = _items.FindIndex(e => e.Id == entity.Id);
      if (index >= 0)
      {
        entity.UpdatedAt = DateTime.UtcNow;
        _items[index] = entity;
      }
      return Task.FromResult(entity);
    }
  }

  public virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      var entity = _items.FirstOrDefault(e => e.Id == id);
      if (entity != null)
      {
        _items.Remove(entity);
      }
      return Task.CompletedTask;
    }
  }

  public virtual Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      return Task.FromResult(_items.Any(e => e.Id == id));
    }
  }

  public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      return Task.FromResult(_items.Count);
    }
  }

  public virtual Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    lock (_lock)
    {
      var compiledPredicate = predicate.Compile();
      return Task.FromResult(_items.Count(compiledPredicate));
    }
  }
}
