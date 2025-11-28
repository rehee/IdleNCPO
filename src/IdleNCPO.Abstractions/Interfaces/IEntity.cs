namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Base interface for all entities
/// </summary>
public interface IEntity
{
  Guid Id { get; set; }
  DateTime CreatedAt { get; set; }
  DateTime? UpdatedAt { get; set; }
}
