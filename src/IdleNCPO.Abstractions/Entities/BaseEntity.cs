using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Entities;

/// <summary>
/// Abstract base class for all entity types
/// </summary>
public abstract class BaseEntity : IEntity
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
}
