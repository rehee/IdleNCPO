using System.Linq.Expressions;
using IdleNCPO.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdleNCPO.Data.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class EfRepository<T> : IRepository<T> where T : class, IEntity
{
  protected readonly DbContext _context;
  protected readonly DbSet<T> _dbSet;

  public EfRepository(DbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
  }

  public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _dbSet.ToListAsync(cancellationToken);
  }

  public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
  }

  public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    entity.CreatedAt = DateTime.UtcNow;
    await _dbSet.AddAsync(entity, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
  {
    entity.UpdatedAt = DateTime.UtcNow;
    _dbSet.Update(entity);
    await _context.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var entity = await GetByIdAsync(id, cancellationToken);
    if (entity != null)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
  }

  public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    return await _dbSet.CountAsync(cancellationToken);
  }

  public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _dbSet.CountAsync(predicate, cancellationToken);
  }
}
