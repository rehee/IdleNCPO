using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.DTOs;

/// <summary>
/// Data transfer object for character information
/// </summary>
public class CharacterDTO
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; }
  public int Experience { get; set; }
  public int Strength { get; set; }
  public int Dexterity { get; set; }
  public int Intelligence { get; set; }
  public int Vitality { get; set; }
  public int CurrentHealth { get; set; }
  public int MaxHealth { get; set; }
  public int CurrentMana { get; set; }
  public int MaxMana { get; set; }
  public List<ItemDTO> Equipment { get; set; } = new();
  public List<SkillDTO> Skills { get; set; } = new();
}
