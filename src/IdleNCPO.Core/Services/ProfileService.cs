using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Profiles;

namespace IdleNCPO.Core.Services;

/// <summary>
/// Service for managing and retrieving profile data
/// </summary>
public class ProfileService
{
  private readonly Dictionary<EnumMap, MapProfile> _mapProfiles = new();
  private readonly Dictionary<EnumMonster, MonsterProfile> _monsterProfiles = new();
  private readonly Dictionary<EnumSkill, SkillProfile> _skillProfiles = new();
  private readonly Dictionary<EnumEquipment, EquipmentProfile> _equipmentProfiles = new();

  public ProfileService()
  {
    InitializeDefaultProfiles();
  }

  private void InitializeDefaultProfiles()
  {
    // Initialize map profiles
    _mapProfiles[EnumMap.StarterVillage] = new MapProfile(
      EnumMap.StarterVillage,
      "新手村",
      "适合初学者的安全区域",
      10, 10, 1, 5,
      new List<WaveDefinition>
      {
        new() { WaveNumber = 1, Monsters = new() { new() { MonsterType = EnumMonster.Skeleton, Count = 2 } } },
        new() { WaveNumber = 2, Monsters = new() { new() { MonsterType = EnumMonster.Skeleton, Count = 3 } } }
      });

    _mapProfiles[EnumMap.DarkCave] = new MapProfile(
      EnumMap.DarkCave,
      "黑暗洞窟",
      "危险的地下洞穴",
      15, 15, 5, 10,
      new List<WaveDefinition>
      {
        new() { WaveNumber = 1, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 3 } } },
        new() { WaveNumber = 2, Monsters = new() { new() { MonsterType = EnumMonster.Spider, Count = 4 } } },
        new() { WaveNumber = 3, Monsters = new() { new() { MonsterType = EnumMonster.Zombie, Count = 2 }, new() { MonsterType = EnumMonster.Spider, Count = 2 } } }
      });

    // Initialize monster profiles
    _monsterProfiles[EnumMonster.Skeleton] = new MonsterProfile(
      EnumMonster.Skeleton, "骷髅", "基础的亡灵生物", 50, 10, 2, 20, EnumDamageType.Physical);
    
    _monsterProfiles[EnumMonster.Zombie] = new MonsterProfile(
      EnumMonster.Zombie, "僵尸", "缓慢但强壮的亡灵", 80, 15, 5, 30, EnumDamageType.Physical);
    
    _monsterProfiles[EnumMonster.Goblin] = new MonsterProfile(
      EnumMonster.Goblin, "哥布林", "敏捷的小型怪物", 40, 12, 1, 25, EnumDamageType.Physical);
    
    _monsterProfiles[EnumMonster.Wolf] = new MonsterProfile(
      EnumMonster.Wolf, "野狼", "快速的野兽", 45, 14, 2, 22, EnumDamageType.Physical);
    
    _monsterProfiles[EnumMonster.Spider] = new MonsterProfile(
      EnumMonster.Spider, "巨型蜘蛛", "有毒的节肢动物", 35, 18, 1, 28, EnumDamageType.Chaos);

    // Initialize skill profiles
    _skillProfiles[EnumSkill.BasicAttack] = new SkillProfile(
      EnumSkill.BasicAttack, "普通攻击", "基础物理攻击", 10, 0, 1, 1, EnumDamageType.Physical);
    
    _skillProfiles[EnumSkill.Fireball] = new SkillProfile(
      EnumSkill.Fireball, "火球术", "发射一团火焰", 25, 15, 3, 5, EnumDamageType.Fire, true, 2);
    
    _skillProfiles[EnumSkill.IceArrow] = new SkillProfile(
      EnumSkill.IceArrow, "冰箭", "发射冰冷的箭矢", 20, 10, 2, 6, EnumDamageType.Cold);
    
    _skillProfiles[EnumSkill.LightningBolt] = new SkillProfile(
      EnumSkill.LightningBolt, "闪电", "召唤闪电打击敌人", 30, 20, 4, 7, EnumDamageType.Lightning);
    
    _skillProfiles[EnumSkill.HealingTouch] = new SkillProfile(
      EnumSkill.HealingTouch, "治疗之触", "恢复生命值", -30, 25, 5, 1);

    // Initialize equipment profiles
    _equipmentProfiles[EnumEquipment.LongSword] = new EquipmentProfile(
      EnumEquipment.LongSword, "长剑", "标准的单手剑", EnumEquipmentSlot.MainHand,
      new Dictionary<EnumAttribute, int> { { EnumAttribute.Strength, 5 } });
    
    _equipmentProfiles[EnumEquipment.ShortSword] = new EquipmentProfile(
      EnumEquipment.ShortSword, "短剑", "轻便的短剑", EnumEquipmentSlot.MainHand,
      new Dictionary<EnumAttribute, int> { { EnumAttribute.Dexterity, 3 }, { EnumAttribute.Strength, 2 } });
    
    _equipmentProfiles[EnumEquipment.Shield] = new EquipmentProfile(
      EnumEquipment.Shield, "盾牌", "提供防护的盾牌", EnumEquipmentSlot.OffHand,
      new Dictionary<EnumAttribute, int> { { EnumAttribute.Armor, 10 } });
    
    _equipmentProfiles[EnumEquipment.ChestArmor] = new EquipmentProfile(
      EnumEquipment.ChestArmor, "胸甲", "保护躯干的护甲", EnumEquipmentSlot.Body,
      new Dictionary<EnumAttribute, int> { { EnumAttribute.Armor, 15 }, { EnumAttribute.Vitality, 3 } });
  }

  public MapProfile? GetMapProfile(EnumMap key) => _mapProfiles.TryGetValue(key, out var profile) ? profile : null;
  public MonsterProfile? GetMonsterProfile(EnumMonster key) => _monsterProfiles.TryGetValue(key, out var profile) ? profile : null;
  public SkillProfile? GetSkillProfile(EnumSkill key) => _skillProfiles.TryGetValue(key, out var profile) ? profile : null;
  public EquipmentProfile? GetEquipmentProfile(EnumEquipment key) => _equipmentProfiles.TryGetValue(key, out var profile) ? profile : null;

  public IEnumerable<MapProfile> GetAllMapProfiles() => _mapProfiles.Values;
  public IEnumerable<MonsterProfile> GetAllMonsterProfiles() => _monsterProfiles.Values;
  public IEnumerable<SkillProfile> GetAllSkillProfiles() => _skillProfiles.Values;
  public IEnumerable<EquipmentProfile> GetAllEquipmentProfiles() => _equipmentProfiles.Values;
}
