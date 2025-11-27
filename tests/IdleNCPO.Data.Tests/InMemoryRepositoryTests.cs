using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Core.Entities;
using IdleNCPO.Data.Repositories;

namespace IdleNCPO.Data.Tests;

public class InMemoryRepositoryTests
{
  private readonly InMemoryRepository<ActorEntity> _repository;

  public InMemoryRepositoryTests()
  {
    _repository = new InMemoryRepository<ActorEntity>();
  }

  [Fact]
  public async Task AddAsync_ShouldAddEntity()
  {
    var entity = new ActorEntity
    {
      Name = "Test Actor",
      Level = 1
    };

    var result = await _repository.AddAsync(entity);

    Assert.NotNull(result);
    Assert.Equal(entity.Id, result.Id);
    Assert.Equal("Test Actor", result.Name);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnEntity()
  {
    var entity = new ActorEntity
    {
      Name = "Test Actor",
      Level = 1
    };
    await _repository.AddAsync(entity);

    var result = await _repository.GetByIdAsync(entity.Id);

    Assert.NotNull(result);
    Assert.Equal(entity.Id, result.Id);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnNullForNonExistent()
  {
    var result = await _repository.GetByIdAsync(Guid.NewGuid());

    Assert.Null(result);
  }

  [Fact]
  public async Task GetAllAsync_ShouldReturnAllEntities()
  {
    await _repository.AddAsync(new ActorEntity { Name = "Actor 1" });
    await _repository.AddAsync(new ActorEntity { Name = "Actor 2" });
    await _repository.AddAsync(new ActorEntity { Name = "Actor 3" });

    var results = await _repository.GetAllAsync();

    Assert.Equal(3, results.Count());
  }

  [Fact]
  public async Task FindAsync_ShouldReturnMatchingEntities()
  {
    await _repository.AddAsync(new ActorEntity { Name = "Actor 1", Level = 1 });
    await _repository.AddAsync(new ActorEntity { Name = "Actor 2", Level = 5 });
    await _repository.AddAsync(new ActorEntity { Name = "Actor 3", Level = 5 });

    var results = await _repository.FindAsync(e => e.Level == 5);

    Assert.Equal(2, results.Count());
  }

  [Fact]
  public async Task UpdateAsync_ShouldUpdateEntity()
  {
    var entity = new ActorEntity
    {
      Name = "Original Name",
      Level = 1
    };
    await _repository.AddAsync(entity);

    entity.Name = "Updated Name";
    entity.Level = 10;
    await _repository.UpdateAsync(entity);

    var result = await _repository.GetByIdAsync(entity.Id);

    Assert.NotNull(result);
    Assert.Equal("Updated Name", result.Name);
    Assert.Equal(10, result.Level);
    Assert.NotNull(result.UpdatedAt);
  }

  [Fact]
  public async Task DeleteAsync_ShouldRemoveEntity()
  {
    var entity = new ActorEntity { Name = "To Delete" };
    await _repository.AddAsync(entity);

    await _repository.DeleteAsync(entity.Id);

    var result = await _repository.GetByIdAsync(entity.Id);
    Assert.Null(result);
  }

  [Fact]
  public async Task ExistsAsync_ShouldReturnTrueForExisting()
  {
    var entity = new ActorEntity { Name = "Existing" };
    await _repository.AddAsync(entity);

    var exists = await _repository.ExistsAsync(entity.Id);

    Assert.True(exists);
  }

  [Fact]
  public async Task ExistsAsync_ShouldReturnFalseForNonExisting()
  {
    var exists = await _repository.ExistsAsync(Guid.NewGuid());

    Assert.False(exists);
  }

  [Fact]
  public async Task CountAsync_ShouldReturnTotalCount()
  {
    await _repository.AddAsync(new ActorEntity { Name = "Actor 1" });
    await _repository.AddAsync(new ActorEntity { Name = "Actor 2" });

    var count = await _repository.CountAsync();

    Assert.Equal(2, count);
  }

  [Fact]
  public async Task CountAsync_WithPredicate_ShouldReturnFilteredCount()
  {
    await _repository.AddAsync(new ActorEntity { Name = "Actor", Level = 1 });
    await _repository.AddAsync(new ActorEntity { Name = "Actor", Level = 5 });
    await _repository.AddAsync(new ActorEntity { Name = "Actor", Level = 5 });

    var count = await _repository.CountAsync(e => e.Level == 5);

    Assert.Equal(2, count);
  }
}
