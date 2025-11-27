using IdleNCPO.Core.Enums;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Base entity class that provides common properties for all entities.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Gets or sets the unique identifier for the entity.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets or sets the date and time when the entity was created.
  /// </summary>
  public DateTime CreatedAt { get; set; }

  /// <summary>
  /// Gets or sets the date and time when the entity was last updated.
  /// </summary>
  public DateTime? UpdatedAt { get; set; }

  /// <summary>
  /// Gets or sets the status of the entity.
  /// </summary>
  public EnumStatus Status { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="BaseEntity"/> class.
  /// </summary>
  protected BaseEntity()
  {
    Id = Guid.NewGuid();
    CreatedAt = DateTime.UtcNow;
    Status = EnumStatus.Active;
  }
}
