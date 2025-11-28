using IdleNCPO.Abstractions.Components;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for actors that can move and interact on a 2D map
/// </summary>
public interface IMapActor : IActor
{
  /// <summary>
  /// Position on the 2D map (double precision)
  /// </summary>
  Position2D Position { get; set; }

  /// <summary>
  /// Radius of the actor (default: 0.5 units)
  /// </summary>
  double Radius { get; }

  /// <summary>
  /// Movement speed in units per tick
  /// </summary>
  double MoveSpeed { get; }

  /// <summary>
  /// Target to move towards (null if not moving)
  /// </summary>
  IMapActor? MoveTarget { get; set; }

  /// <summary>
  /// Whether the actor is currently in collision with another actor
  /// </summary>
  bool IsColliding { get; set; }

  /// <summary>
  /// Calculate distance to another actor (center to center minus both radii)
  /// </summary>
  double DistanceTo(IMapActor other);

  /// <summary>
  /// Get the grid cell this actor occupies
  /// </summary>
  (int GridX, int GridY) GetGridCell();

  /// <summary>
  /// Move towards the target position by speed amount
  /// </summary>
  void MoveTowards(Position2D target, double speed);
}
