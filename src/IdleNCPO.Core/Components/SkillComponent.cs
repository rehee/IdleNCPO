using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Core.Components;

/// <summary>
/// IdleComponent for skill runtime data
/// </summary>
public class SkillIdleComponent : IdleComponent<EnumSkill>
{
  public override EnumSkill ProfileKey { get; protected set; }
  
  public Guid Id { get; set; }
  public int Level { get; set; }
  public int CurrentCooldown { get; set; }
  public int BaseDamage { get; set; }
  public int ManaCost { get; set; }
  public int Cooldown { get; set; }
  public int Range { get; set; }
  public EnumDamageType DamageType { get; set; }
  public bool IsAreaOfEffect { get; set; }
  public int AreaRadius { get; set; }
  public List<EnumSupportSkill> LinkedSupports { get; set; } = new();

  public SkillIdleComponent(EnumSkill profileKey)
  {
    ProfileKey = profileKey;
    Id = Guid.NewGuid();
  }

  public bool IsReady => CurrentCooldown <= 0;

  public void Use()
  {
    if (IsReady)
    {
      CurrentCooldown = Cooldown;
    }
  }

  public void Tick()
  {
    if (CurrentCooldown > 0)
    {
      CurrentCooldown--;
    }
  }

  public int CalculateDamage()
  {
    var damage = BaseDamage + (Level * 5);
    foreach (var support in LinkedSupports)
    {
      if (support == EnumSupportSkill.IncreasedDamage)
      {
        damage = (int)(damage * 1.3);
      }
    }
    return damage;
  }
}
