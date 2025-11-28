using IdleNCPO.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdleNCPO.Data.Contexts;

/// <summary>
/// Application database context for EF Core
/// </summary>
public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
  {
  }

  public DbSet<ActorEntity> Actors => Set<ActorEntity>();
  public DbSet<ItemEntity> Items => Set<ItemEntity>();
  public DbSet<SkillEntity> Skills => Set<SkillEntity>();
  public DbSet<BattleReplayEntity> BattleReplays => Set<BattleReplayEntity>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ActorEntity>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
      entity.HasIndex(e => e.UserId);
    });

    modelBuilder.Entity<ItemEntity>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.HasIndex(e => e.OwnerId);
      entity.Property(e => e.Attributes)
        .HasConversion(
          v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
          v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<IdleNCPO.Abstractions.Enums.EnumAttribute, int>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new());
    });

    modelBuilder.Entity<SkillEntity>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.HasIndex(e => e.OwnerId);
      entity.Property(e => e.LinkedSupports)
        .HasConversion(
          v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
          v => System.Text.Json.JsonSerializer.Deserialize<List<IdleNCPO.Abstractions.Enums.EnumSupportSkill>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new());
    });

    modelBuilder.Entity<BattleReplayEntity>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.HasIndex(e => e.PlayerId);
      entity.Property(e => e.ItemsDropped)
        .HasConversion(
          v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
          v => System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new());
    });
  }
}
