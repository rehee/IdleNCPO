namespace IdleNCPO.Abstractions.Components;

/// <summary>
/// Represents a 2D position with double precision coordinates
/// </summary>
public struct Position2D
{
  public double X { get; set; }
  public double Y { get; set; }

  public Position2D(double x, double y)
  {
    X = x;
    Y = y;
  }

  /// <summary>
  /// Calculate distance to another position
  /// </summary>
  public double DistanceTo(Position2D other)
  {
    var dx = X - other.X;
    var dy = Y - other.Y;
    return Math.Sqrt(dx * dx + dy * dy);
  }

  /// <summary>
  /// Get the grid cell coordinates (integer) for this position
  /// </summary>
  public (int GridX, int GridY) GetGridCell()
  {
    return ((int)Math.Floor(X), (int)Math.Floor(Y));
  }

  /// <summary>
  /// Normalize direction vector
  /// </summary>
  public Position2D Normalize()
  {
    var length = Math.Sqrt(X * X + Y * Y);
    if (length == 0) return new Position2D(0, 0);
    return new Position2D(X / length, Y / length);
  }

  /// <summary>
  /// Get direction vector towards target
  /// </summary>
  public Position2D DirectionTo(Position2D target)
  {
    var dx = target.X - X;
    var dy = target.Y - Y;
    var length = Math.Sqrt(dx * dx + dy * dy);
    if (length == 0) return new Position2D(0, 0);
    return new Position2D(dx / length, dy / length);
  }

  public static Position2D operator +(Position2D a, Position2D b)
  {
    return new Position2D(a.X + b.X, a.Y + b.Y);
  }

  public static Position2D operator -(Position2D a, Position2D b)
  {
    return new Position2D(a.X - b.X, a.Y - b.Y);
  }

  public static Position2D operator *(Position2D a, double scalar)
  {
    return new Position2D(a.X * scalar, a.Y * scalar);
  }

  public override string ToString()
  {
    return $"({X:F2}, {Y:F2})";
  }

  public override bool Equals(object? obj)
  {
    if (obj is Position2D other)
    {
      return Math.Abs(X - other.X) < 0.0001 && Math.Abs(Y - other.Y) < 0.0001;
    }
    return false;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(X, Y);
  }
}
