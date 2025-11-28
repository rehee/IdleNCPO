using IdleNCPO.Abstractions.Components;

namespace IdleNCPO.Core.Tests;

public class Position2DTests
{
  [Fact]
  public void Position2D_Constructor_ShouldSetValues()
  {
    var position = new Position2D(5.5, 10.5);

    Assert.Equal(5.5, position.X);
    Assert.Equal(10.5, position.Y);
  }

  [Fact]
  public void Position2D_DistanceTo_ShouldCalculateCorrectly()
  {
    var p1 = new Position2D(0, 0);
    var p2 = new Position2D(3, 4);

    var distance = p1.DistanceTo(p2);

    Assert.Equal(5.0, distance, 5);
  }

  [Fact]
  public void Position2D_GetGridCell_ShouldReturnCorrectCell()
  {
    var position = new Position2D(5.7, 10.3);

    var (gridX, gridY) = position.GetGridCell();

    Assert.Equal(5, gridX);
    Assert.Equal(10, gridY);
  }

  [Fact]
  public void Position2D_GetGridCell_ShouldHandleNegativeValues()
  {
    var position = new Position2D(-0.5, -0.5);

    var (gridX, gridY) = position.GetGridCell();

    Assert.Equal(-1, gridX);
    Assert.Equal(-1, gridY);
  }

  [Fact]
  public void Position2D_Normalize_ShouldReturnUnitVector()
  {
    var position = new Position2D(3, 4);

    var normalized = position.Normalize();

    var length = Math.Sqrt(normalized.X * normalized.X + normalized.Y * normalized.Y);
    Assert.Equal(1.0, length, 5);
  }

  [Fact]
  public void Position2D_Normalize_ZeroVector_ShouldReturnZero()
  {
    var position = new Position2D(0, 0);

    var normalized = position.Normalize();

    Assert.Equal(0, normalized.X);
    Assert.Equal(0, normalized.Y);
  }

  [Fact]
  public void Position2D_DirectionTo_ShouldReturnNormalizedDirection()
  {
    var p1 = new Position2D(0, 0);
    var p2 = new Position2D(10, 0);

    var direction = p1.DirectionTo(p2);

    Assert.Equal(1.0, direction.X, 5);
    Assert.Equal(0.0, direction.Y, 5);
  }

  [Fact]
  public void Position2D_Addition_ShouldWork()
  {
    var p1 = new Position2D(1, 2);
    var p2 = new Position2D(3, 4);

    var result = p1 + p2;

    Assert.Equal(4.0, result.X);
    Assert.Equal(6.0, result.Y);
  }

  [Fact]
  public void Position2D_Subtraction_ShouldWork()
  {
    var p1 = new Position2D(5, 7);
    var p2 = new Position2D(2, 3);

    var result = p1 - p2;

    Assert.Equal(3.0, result.X);
    Assert.Equal(4.0, result.Y);
  }

  [Fact]
  public void Position2D_ScalarMultiplication_ShouldWork()
  {
    var position = new Position2D(2, 3);

    var result = position * 2.5;

    Assert.Equal(5.0, result.X);
    Assert.Equal(7.5, result.Y);
  }

  [Fact]
  public void Position2D_ToString_ShouldFormatCorrectly()
  {
    var position = new Position2D(5.5, 10.25);

    var str = position.ToString();

    Assert.Contains("5.50", str);
    Assert.Contains("10.25", str);
  }

  [Fact]
  public void Position2D_Equals_ShouldCompareCorrectly()
  {
    var p1 = new Position2D(5.0, 10.0);
    var p2 = new Position2D(5.0, 10.0);
    var p3 = new Position2D(5.0, 11.0);

    Assert.True(p1.Equals(p2));
    Assert.False(p1.Equals(p3));
  }
}
