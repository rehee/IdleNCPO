using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Profiles;

namespace IdleNCPO.Core.Services;

/// <summary>
/// Service for managing and retrieving IdleProfile data
/// </summary>
public class ProfileService
{
  private readonly Dictionary<EnumMap, MapIdleProfile> _mapProfiles = new();
  private readonly Dictionary<EnumMonster, MonsterIdleProfile> _monsterProfiles = new();
  private readonly Dictionary<EnumSkill, SkillIdleProfile> _skillProfiles = new();
  private readonly Dictionary<EnumItem, ItemIdleProfile> _itemProfiles = new();

  public ProfileService()
  {
    InitializeDefaultProfiles();
  }

  private void InitializeDefaultProfiles()
  {
    // Initialize map profiles
    RegisterMapProfile(new StarterVillageMapProfile());
    RegisterMapProfile(new DarkCaveMapProfile());

    // Initialize monster profiles
    RegisterMonsterProfile(new SkeletonMonsterProfile());
    RegisterMonsterProfile(new ZombieMonsterProfile());
    RegisterMonsterProfile(new GoblinMonsterProfile());
    RegisterMonsterProfile(new WolfMonsterProfile());
    RegisterMonsterProfile(new SpiderMonsterProfile());

    // Initialize skill profiles
    RegisterSkillProfile(new BasicAttackSkillProfile());
    RegisterSkillProfile(new FireballSkillProfile());
    RegisterSkillProfile(new IceArrowSkillProfile());
    RegisterSkillProfile(new LightningBoltSkillProfile());
    RegisterSkillProfile(new HealingTouchSkillProfile());

    // Initialize item profiles
    RegisterItemProfile(new LongSwordItemProfile());
    RegisterItemProfile(new ShortSwordItemProfile());
    RegisterItemProfile(new DaggerItemProfile());
    RegisterItemProfile(new StaffItemProfile());
    RegisterItemProfile(new BowItemProfile());
    RegisterItemProfile(new ShieldItemProfile());
    RegisterItemProfile(new HelmetItemProfile());
    RegisterItemProfile(new ChestArmorItemProfile());
    RegisterItemProfile(new BootsItemProfile());
    RegisterItemProfile(new GlovesItemProfile());
    RegisterItemProfile(new RingItemProfile());
    RegisterItemProfile(new AmuletItemProfile());
    RegisterItemProfile(new BeltItemProfile());
    RegisterItemProfile(new BrokenSwordJunkProfile());
    RegisterItemProfile(new RustyHelmetJunkProfile());
    RegisterItemProfile(new MonsterBoneJunkProfile());
  }

  private void RegisterMapProfile(MapIdleProfile profile)
  {
    _mapProfiles[profile.Key] = profile;
  }

  private void RegisterMonsterProfile(MonsterIdleProfile profile)
  {
    _monsterProfiles[profile.Key] = profile;
  }

  private void RegisterSkillProfile(SkillIdleProfile profile)
  {
    _skillProfiles[profile.Key] = profile;
  }

  private void RegisterItemProfile(ItemIdleProfile profile)
  {
    _itemProfiles[profile.Key] = profile;
  }

  public MapIdleProfile? GetMapProfile(EnumMap key) => _mapProfiles.TryGetValue(key, out var profile) ? profile : null;
  public MonsterIdleProfile? GetMonsterProfile(EnumMonster key) => _monsterProfiles.TryGetValue(key, out var profile) ? profile : null;
  public SkillIdleProfile? GetSkillProfile(EnumSkill key) => _skillProfiles.TryGetValue(key, out var profile) ? profile : null;
  public ItemIdleProfile? GetItemProfile(EnumItem key) => _itemProfiles.TryGetValue(key, out var profile) ? profile : null;
  public EquipmentIdleProfile? GetEquipmentProfile(EnumItem key) => _itemProfiles.TryGetValue(key, out var profile) && profile is EquipmentIdleProfile equipment ? equipment : null;

  public IEnumerable<MapIdleProfile> GetAllMapProfiles() => _mapProfiles.Values;
  public IEnumerable<MonsterIdleProfile> GetAllMonsterProfiles() => _monsterProfiles.Values;
  public IEnumerable<SkillIdleProfile> GetAllSkillProfiles() => _skillProfiles.Values;
  public IEnumerable<ItemIdleProfile> GetAllItemProfiles() => _itemProfiles.Values;
  public IEnumerable<EquipmentIdleProfile> GetAllEquipmentProfiles() => _itemProfiles.Values.OfType<EquipmentIdleProfile>();
}
