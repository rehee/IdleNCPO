using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// Represents a 2D game map with grid-based collision detection
/// Map constants:
/// - Default size: 20x20 units
/// - Standard radius: 0.5 units
/// - Display: 1 unit = 32 pixels
/// </summary>
public class GameMap2D
{
  /// <summary>
  /// Default map width in units
  /// </summary>
  public const int DefaultWidth = 20;

  /// <summary>
  /// Default map height in units
  /// </summary>
  public const int DefaultHeight = 20;

  /// <summary>
  /// Standard radius for actors in units
  /// </summary>
  public const double StandardRadius = 0.5;

  /// <summary>
  /// Pixels per unit for display purposes
  /// </summary>
  public const int PixelsPerUnit = 32;

  /// <summary>
  /// Collision threshold (1/4 of standard radius)
  /// </summary>
  public const double CollisionThreshold = StandardRadius / 4.0;

  /// <summary>
  /// Map width in units
  /// </summary>
  public int Width { get; }

  /// <summary>
  /// Map height in units
  /// </summary>
  public int Height { get; }

  /// <summary>
  /// Grid cells for spatial partitioning
  /// </summary>
  private readonly GridCell[,] _grid;

  /// <summary>
  /// All actors on the map
  /// </summary>
  private readonly List<IMapActor> _actors = new();

  /// <summary>
  /// Track which cell each actor is currently in
  /// </summary>
  private readonly Dictionary<IMapActor, GridCell?> _actorCells = new();

  public GameMap2D(int width = DefaultWidth, int height = DefaultHeight)
  {
    Width = width;
    Height = height;
    _grid = new GridCell[width, height];

    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        _grid[x, y] = new GridCell(x, y);
      }
    }
  }

  /// <summary>
  /// Get pixel width for display
  /// </summary>
  public int PixelWidth => Width * PixelsPerUnit;

  /// <summary>
  /// Get pixel height for display
  /// </summary>
  public int PixelHeight => Height * PixelsPerUnit;

  /// <summary>
  /// Get grid cell at position
  /// </summary>
  public GridCell? GetCell(int x, int y)
  {
    if (x < 0 || x >= Width || y < 0 || y >= Height)
      return null;
    return _grid[x, y];
  }

  /// <summary>
  /// Get grid cell at position
  /// </summary>
  public GridCell? GetCell(Position2D position)
  {
    var (gridX, gridY) = position.GetGridCell();
    return GetCell(gridX, gridY);
  }

  /// <summary>
  /// Add an actor to the map
  /// </summary>
  public void AddActor(IMapActor actor)
  {
    if (_actors.Contains(actor)) return;

    _actors.Add(actor);
    var cell = GetCell(actor.Position);
    cell?.AddActor(actor);
    _actorCells[actor] = cell;
  }

  /// <summary>
  /// Remove an actor from the map
  /// </summary>
  public void RemoveActor(IMapActor actor)
  {
    _actors.Remove(actor);
    if (_actorCells.TryGetValue(actor, out var oldCell))
    {
      oldCell?.RemoveActor(actor);
      _actorCells.Remove(actor);
    }
  }

  /// <summary>
  /// Get all actors on the map
  /// </summary>
  public IReadOnlyList<IMapActor> GetAllActors()
  {
    return _actors.AsReadOnly();
  }

  /// <summary>
  /// Get all alive actors on the map
  /// </summary>
  public IEnumerable<IMapActor> GetAliveActors()
  {
    return _actors.Where(a => a.IsAlive);
  }

  /// <summary>
  /// Update actor's cell after movement (efficient - only updates if cell changed)
  /// </summary>
  public void UpdateActorCell(IMapActor actor)
  {
    var newCell = GetCell(actor.Position);
    
    // Get the old cell from tracking
    _actorCells.TryGetValue(actor, out var oldCell);
    
    // Only update if cell changed
    if (oldCell != newCell)
    {
      oldCell?.RemoveActor(actor);
      newCell?.AddActor(actor);
      _actorCells[actor] = newCell;
    }
  }

  /// <summary>
  /// Get surrounding cells for collision detection (3x3 grid)
  /// </summary>
  public IEnumerable<GridCell> GetSurroundingCells((int GridX, int GridY) center)
  {
    for (int dx = -1; dx <= 1; dx++)
    {
      for (int dy = -1; dy <= 1; dy++)
      {
        var cell = GetCell(center.GridX + dx, center.GridY + dy);
        if (cell != null)
        {
          yield return cell;
        }
      }
    }
  }

  /// <summary>
  /// Get nearby actors for collision check based on grid cells
  /// </summary>
  public IEnumerable<IMapActor> GetNearbyActors(IMapActor actor)
  {
    var actorCell = actor.GetGridCell();
    var nearbyActors = new HashSet<IMapActor>();

    foreach (var cell in GetSurroundingCells(actorCell))
    {
      foreach (var other in cell.Actors)
      {
        if (other != actor && other.IsAlive)
        {
          nearbyActors.Add(other);
        }
      }
    }

    return nearbyActors;
  }

  /// <summary>
  /// Check and update collisions for all actors
  /// Returns list of colliding actor pairs
  /// </summary>
  public List<(IMapActor, IMapActor)> CheckCollisions()
  {
    var collisions = new List<(IMapActor, IMapActor)>();

    foreach (var actor in _actors.Where(a => a.IsAlive))
    {
      actor.IsColliding = false;
    }

    foreach (var actor in _actors.Where(a => a.IsAlive))
    {
      foreach (var other in GetNearbyActors(actor))
      {
        if (other == actor) continue;

        var distance = actor.DistanceTo(other);
        if (distance < CollisionThreshold)
        {
          actor.IsColliding = true;
          other.IsColliding = true;

          // Avoid duplicate pairs
          if (!collisions.Any(c => (c.Item1 == actor && c.Item2 == other) ||
                                   (c.Item1 == other && c.Item2 == actor)))
          {
            collisions.Add((actor, other));
          }
        }
      }
    }

    return collisions;
  }

  /// <summary>
  /// Check if an actor is within attack range of another
  /// </summary>
  public bool IsInAttackRange(IMapActor attacker, IMapActor target, double attackRange)
  {
    var distance = attacker.DistanceTo(target);
    return distance <= attackRange;
  }

  /// <summary>
  /// Get all actors within range of a position
  /// </summary>
  public IEnumerable<IMapActor> GetActorsInRange(Position2D center, double range)
  {
    foreach (var actor in _actors.Where(a => a.IsAlive))
    {
      var distance = center.DistanceTo(actor.Position) - actor.Radius;
      if (distance <= range)
      {
        yield return actor;
      }
    }
  }

  /// <summary>
  /// Generate random position within map bounds
  /// </summary>
  public Position2D GetRandomPosition(Random random)
  {
    var x = random.NextDouble() * (Width - 1) + 0.5;
    var y = random.NextDouble() * (Height - 1) + 0.5;
    return new Position2D(x, y);
  }

  /// <summary>
  /// Clamp position to map bounds considering radius
  /// </summary>
  public Position2D ClampToMap(Position2D position, double radius)
  {
    var x = Math.Clamp(position.X, radius, Width - radius);
    var y = Math.Clamp(position.Y, radius, Height - radius);
    return new Position2D(x, y);
  }

  /// <summary>
  /// Convert position to pixel coordinates for display
  /// </summary>
  public static (int PixelX, int PixelY) ToPixelCoordinates(Position2D position)
  {
    return ((int)(position.X * PixelsPerUnit), (int)(position.Y * PixelsPerUnit));
  }

  /// <summary>
  /// Convert pixel coordinates to map position
  /// </summary>
  public static Position2D FromPixelCoordinates(int pixelX, int pixelY)
  {
    return new Position2D((double)pixelX / PixelsPerUnit, (double)pixelY / PixelsPerUnit);
  }

  /// <summary>
  /// Process movement for all actors towards their targets
  /// </summary>
  public void ProcessMovement()
  {
    foreach (var actor in _actors.Where(a => a.IsAlive && a.MoveTarget != null && !a.IsColliding))
    {
      if (actor.MoveTarget == null || !actor.MoveTarget.IsAlive) continue;

      var targetPosition = actor.MoveTarget.Position;
      actor.MoveTowards(targetPosition, actor.MoveSpeed);

      // Clamp to map bounds
      actor.Position = ClampToMap(actor.Position, actor.Radius);

      // Update grid cell
      UpdateActorCell(actor);
    }
  }
}
