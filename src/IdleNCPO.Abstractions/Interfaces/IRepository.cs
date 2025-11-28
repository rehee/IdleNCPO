using System.Linq.Expressions;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class, IEntity
{
  Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
  Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
  Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
  Task<int> CountAsync(CancellationToken cancellationToken = default);
  Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
