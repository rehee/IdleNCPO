using IdleNCPO.Abstractions.Components;
using IdleNCPO.Core.Components;

namespace IdleNCPO.Core.Tests;

public class GameMap2DTests
{
  [Fact]
  public void GameMap2D_ShouldHaveDefaultDimensions()
  {
    var map = new GameMap2D();

    Assert.Equal(GameMap2D.DefaultWidth, map.Width);
    Assert.Equal(GameMap2D.DefaultHeight, map.Height);
  }

  [Fact]
  public void GameMap2D_ShouldHaveCorrectPixelDimensions()
  {
    var map = new GameMap2D();

    Assert.Equal(GameMap2D.DefaultWidth * GameMap2D.PixelsPerUnit, map.PixelWidth);
    Assert.Equal(GameMap2D.DefaultHeight * GameMap2D.PixelsPerUnit, map.PixelHeight);
  }

  [Fact]
  public void GameMap2D_StandardRadius_ShouldBe0_5()
  {
    Assert.Equal(0.5, GameMap2D.StandardRadius);
  }

  [Fact]
  public void GameMap2D_PixelsPerUnit_ShouldBe32()
  {
    Assert.Equal(32, GameMap2D.PixelsPerUnit);
  }

  [Fact]
  public void GameMap2D_CollisionThreshold_ShouldBeQuarterOfRadius()
  {
    Assert.Equal(GameMap2D.StandardRadius / 4.0, GameMap2D.CollisionThreshold);
  }

  [Fact]
  public void GameMap2D_GetCell_ShouldReturnCell()
  {
    var map = new GameMap2D();

    var cell = map.GetCell(5, 5);

    Assert.NotNull(cell);
    Assert.Equal(5, cell.X);
    Assert.Equal(5, cell.Y);
  }

  [Fact]
  public void GameMap2D_GetCell_OutOfBounds_ShouldReturnNull()
  {
    var map = new GameMap2D();

    Assert.Null(map.GetCell(-1, 0));
    Assert.Null(map.GetCell(0, -1));
    Assert.Null(map.GetCell(20, 0));
    Assert.Null(map.GetCell(0, 20));
  }

  [Fact]
  public void GameMap2D_AddActor_ShouldAddToActorsList()
  {
    var map = new GameMap2D();
    var monster = CreateTestMonster(5.5, 5.5);

    map.AddActor(monster);

    Assert.Single(map.GetAllActors());
  }

  [Fact]
  public void GameMap2D_RemoveActor_ShouldRemoveFromList()
  {
    var map = new GameMap2D();
    var monster = CreateTestMonster(5.5, 5.5);
    map.AddActor(monster);

    map.RemoveActor(monster);

    Assert.Empty(map.GetAllActors());
  }

  [Fact]
  public void GameMap2D_GetAliveActors_ShouldFilterDeadActors()
  {
    var map = new GameMap2D();
    var monster1 = CreateTestMonster(5.5, 5.5);
    var monster2 = CreateTestMonster(6.5, 6.5);
    monster2.CurrentHealth = 0;

    map.AddActor(monster1);
    map.AddActor(monster2);

    var aliveActors = map.GetAliveActors().ToList();
    Assert.Single(aliveActors);
  }

  [Fact]
  public void GameMap2D_CheckCollisions_ShouldDetectCollision()
  {
    var map = new GameMap2D();
    var monster1 = CreateTestMonster(5.0, 5.0);
    var monster2 = CreateTestMonster(5.1, 5.0); // Very close, should collide

    map.AddActor(monster1);
    map.AddActor(monster2);

    var collisions = map.CheckCollisions();

    Assert.Single(collisions);
    Assert.True(monster1.IsColliding);
    Assert.True(monster2.IsColliding);
  }

  [Fact]
  public void GameMap2D_CheckCollisions_ShouldNotDetectCollisionWhenFarApart()
  {
    var map = new GameMap2D();
    var monster1 = CreateTestMonster(5.0, 5.0);
    var monster2 = CreateTestMonster(10.0, 10.0); // Far apart

    map.AddActor(monster1);
    map.AddActor(monster2);

    var collisions = map.CheckCollisions();

    Assert.Empty(collisions);
    Assert.False(monster1.IsColliding);
    Assert.False(monster2.IsColliding);
  }

  [Fact]
  public void GameMap2D_IsInAttackRange_ShouldReturnTrueWhenInRange()
  {
    var map = new GameMap2D();
    var attacker = CreateTestMonster(5.0, 5.0);
    var target = CreateTestMonster(6.0, 5.0);

    map.AddActor(attacker);
    map.AddActor(target);

    // Distance is 1.0, minus both radii (0.5 + 0.5) = 0
    // Attack range of 2 should be in range
    Assert.True(map.IsInAttackRange(attacker, target, 2.0));
  }

  [Fact]
  public void GameMap2D_IsInAttackRange_ShouldReturnFalseWhenOutOfRange()
  {
    var map = new GameMap2D();
    var attacker = CreateTestMonster(5.0, 5.0);
    var target = CreateTestMonster(15.0, 5.0);

    map.AddActor(attacker);
    map.AddActor(target);

    // Distance is 10.0, minus both radii = 9.0
    // Attack range of 2 should not be in range
    Assert.False(map.IsInAttackRange(attacker, target, 2.0));
  }

  [Fact]
  public void GameMap2D_GetActorsInRange_ShouldReturnActorsInRange()
  {
    var map = new GameMap2D();
    var monster1 = CreateTestMonster(5.0, 5.0);
    var monster2 = CreateTestMonster(6.0, 5.0);
    var monster3 = CreateTestMonster(15.0, 15.0);

    map.AddActor(monster1);
    map.AddActor(monster2);
    map.AddActor(monster3);

    var center = new Position2D(5.5, 5.0);
    var actorsInRange = map.GetActorsInRange(center, 2.0).ToList();

    Assert.Equal(2, actorsInRange.Count);
  }

  [Fact]
  public void GameMap2D_ClampToMap_ShouldClampPosition()
  {
    var map = new GameMap2D();
    var position = new Position2D(-1.0, 25.0);

    var clamped = map.ClampToMap(position, 0.5);

    Assert.Equal(0.5, clamped.X);
    Assert.Equal(19.5, clamped.Y);
  }

  [Fact]
  public void GameMap2D_ToPixelCoordinates_ShouldConvertCorrectly()
  {
    var position = new Position2D(5.0, 10.0);

    var (pixelX, pixelY) = GameMap2D.ToPixelCoordinates(position);

    Assert.Equal(160, pixelX);
    Assert.Equal(320, pixelY);
  }

  [Fact]
  public void GameMap2D_FromPixelCoordinates_ShouldConvertCorrectly()
  {
    var position = GameMap2D.FromPixelCoordinates(160, 320);

    Assert.Equal(5.0, position.X);
    Assert.Equal(10.0, position.Y);
  }

  [Fact]
  public void GameMap2D_GetRandomPosition_ShouldBeWithinBounds()
  {
    var map = new GameMap2D();
    var random = new Random(12345);

    for (int i = 0; i < 100; i++)
    {
      var position = map.GetRandomPosition(random);

      Assert.True(position.X >= 0.5);
      Assert.True(position.X <= map.Width - 0.5);
      Assert.True(position.Y >= 0.5);
      Assert.True(position.Y <= map.Height - 0.5);
    }
  }

  private MonsterIdleComponent CreateTestMonster(double x, double y)
  {
    return new MonsterIdleComponent(IdleNCPO.Abstractions.Enums.EnumMonster.Skeleton)
    {
      Position = new Position2D(x, y),
      MaxHealth = 100,
      CurrentHealth = 100
    };
  }
}
