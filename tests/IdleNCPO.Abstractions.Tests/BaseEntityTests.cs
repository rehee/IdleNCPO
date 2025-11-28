using IdleNCPO.Abstractions.Entities;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Abstractions.Tests;

public class BaseEntityTests
{
  private class TestEntity : BaseEntity
  {
  }

  [Fact]
  public void BaseEntity_ShouldImplementIEntity()
  {
    var entity = new TestEntity();
    Assert.IsAssignableFrom<IEntity>(entity);
  }

  [Fact]
  public void BaseEntity_ShouldHaveDefaultId()
  {
    var entity = new TestEntity();
    Assert.NotEqual(Guid.Empty, entity.Id);
  }

  [Fact]
  public void BaseEntity_ShouldHaveCreatedAtSet()
  {
    var before = DateTime.UtcNow;
    var entity = new TestEntity();
    var after = DateTime.UtcNow;

    Assert.InRange(entity.CreatedAt, before.AddSeconds(-1), after.AddSeconds(1));
  }

  [Fact]
  public void BaseEntity_ShouldHaveNullUpdatedAtByDefault()
  {
    var entity = new TestEntity();
    Assert.Null(entity.UpdatedAt);
  }
}
