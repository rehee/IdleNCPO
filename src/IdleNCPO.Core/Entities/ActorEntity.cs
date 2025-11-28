using IdleNCPO.Abstractions.Entities;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Entity for storing player/actor base information
/// </summary>
public class ActorEntity : BaseEntity
{
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; } = 1;
  public int Experience { get; set; }
  public int Strength { get; set; }
  public int Dexterity { get; set; }
  public int Intelligence { get; set; }
  public int Vitality { get; set; }
  public Guid? UserId { get; set; }
}
