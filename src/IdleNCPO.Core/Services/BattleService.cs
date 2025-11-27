using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Core.Components;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Core.Profiles;

namespace IdleNCPO.Core.Services;

/// <summary>
/// Service for managing battle logic
/// </summary>
public class BattleService : IBattle
{
  private readonly ProfileService _profileService;
  private readonly Random _battleRandom;
  private readonly Random _lootRandom;
  
  public int BattleSeed { get; }
  public int LootSeed { get; }
  public int CurrentTick { get; private set; }
  public bool IsFinished { get; private set; }
  public bool IsVictory { get; private set; }
  
  public MapIdleComponent Map { get; private set; }
  public int ExperienceGained { get; private set; }
  public List<ItemDTO> ItemsDropped { get; } = new();

  public BattleService(ProfileService profileService, BattleSeedDTO seed)
  {
    _profileService = profileService;
    BattleSeed = seed.BattleSeed;
    LootSeed = seed.LootSeed;
    _battleRandom = new Random(BattleSeed);
    _lootRandom = new Random(LootSeed);
    
    Map = InitializeMap(seed);
    CurrentTick = 0;
    IsFinished = false;
    IsVictory = false;
  }

  private MapIdleComponent InitializeMap(BattleSeedDTO seed)
  {
    var mapProfile = _profileService.GetMapProfile(seed.MapKey);
    if (mapProfile == null)
      throw new ArgumentException($"Map profile not found: {seed.MapKey}");

    var map = new MapIdleComponent(seed.MapKey)
    {
      Width = mapProfile.Width,
      Height = mapProfile.Height,
      CurrentWave = 1,
      TotalWaves = mapProfile.Waves.Count,
      Player = CreatePlayerComponent(seed.Player)
    };

    // Spawn initial wave
    SpawnWave(map, mapProfile, 1, seed.MapLevel);

    return map;
  }

  private CharacterIdleComponent CreatePlayerComponent(CharacterDTO dto)
  {
    var character = new CharacterIdleComponent
    {
      Id = dto.Id,
      Name = dto.Name,
      Level = dto.Level,
      Experience = dto.Experience,
      Strength = dto.Strength,
      Dexterity = dto.Dexterity,
      Intelligence = dto.Intelligence,
      Vitality = dto.Vitality,
      X = 0,
      Y = 0
    };
    
    character.CurrentHealth = character.MaxHealth;
    character.CurrentMana = character.MaxMana;

    // Add skills
    foreach (var skillDto in dto.Skills)
    {
      var skillProfile = _profileService.GetSkillProfile(skillDto.SkillType);
      if (skillProfile != null)
      {
        var skill = new SkillIdleComponent(skillDto.SkillType)
        {
          Id = skillDto.Id,
          Level = skillDto.Level,
          BaseDamage = skillProfile.BaseDamage,
          ManaCost = skillProfile.ManaCost,
          Cooldown = skillProfile.Cooldown,
          Range = skillProfile.Range,
          DamageType = skillProfile.DamageType,
          IsAreaOfEffect = skillProfile.IsAreaOfEffect,
          AreaRadius = skillProfile.AreaRadius,
          LinkedSupports = skillDto.LinkedSupports
        };
        character.Skills.Add(skill);
      }
    }

    // Add equipment
    foreach (var itemDto in dto.Equipment)
    {
      var equipmentProfile = _profileService.GetEquipmentProfile(itemDto.ItemType);
      if (equipmentProfile != null)
      {
        var equipment = new ItemIdleComponent(itemDto.ItemType)
        {
          Id = itemDto.Id,
          Category = EnumItemCategory.Equipment,
          Slot = itemDto.EquippedSlot ?? equipmentProfile.Slot,
          ItemLevel = itemDto.ItemLevel,
          Attributes = itemDto.Attributes
        };
        character.Equipment.Add(equipment);
      }
    }

    return character;
  }

  private void SpawnWave(MapIdleComponent map, MapIdleProfile mapProfile, int waveNumber, int mapLevel)
  {
    var waveDefinition = mapProfile.Waves.FirstOrDefault(w => w.WaveNumber == waveNumber);
    if (waveDefinition == null) return;

    map.Monsters.Clear();
    
    foreach (var spawn in waveDefinition.Monsters)
    {
      var monsterProfile = _profileService.GetMonsterProfile(spawn.MonsterType);
      if (monsterProfile == null) continue;

      for (int i = 0; i < spawn.Count; i++)
      {
        var level = mapLevel + spawn.LevelModifier;
        var monster = new MonsterIdleComponent(spawn.MonsterType)
        {
          Name = monsterProfile.Name,
          Level = level,
          MaxHealth = monsterProfile.BaseHealth + (level * 10),
          BaseDamage = monsterProfile.BaseDamage + (level * 2),
          BaseArmor = monsterProfile.BaseArmor + (level / 2),
          BaseExperience = monsterProfile.BaseExperience,
          DamageType = monsterProfile.DamageType,
          X = _battleRandom.Next(map.Width),
          Y = _battleRandom.Next(map.Height)
        };
        monster.CurrentHealth = monster.MaxHealth;
        monster.CurrentMana = monster.MaxMana;
        
        map.Monsters.Add(monster);
      }
    }
  }

  public void ProcessTick()
  {
    if (IsFinished) return;

    CurrentTick++;

    // Process player actions
    ProcessPlayerTurn();

    // Process monster actions
    ProcessMonsterTurns();

    // Check win/lose conditions
    CheckBattleEnd();

    // Update cooldowns
    UpdateCooldowns();
  }

  private void ProcessPlayerTurn()
  {
    if (Map.Player == null || !Map.Player.IsAlive) return;

    var aliveMonsters = Map.GetAliveMonsters();
    if (aliveMonsters.Count == 0) return;

    // Find best skill to use
    var target = aliveMonsters.First();
    var skill = Map.Player.Skills.FirstOrDefault(s => s.IsReady && Map.Player.CurrentMana >= s.ManaCost);

    if (skill != null)
    {
      UseSkill(Map.Player, target, skill);
    }
  }

  private void UseSkill(CharacterIdleComponent user, MonsterIdleComponent target, SkillIdleComponent skill)
  {
    user.CurrentMana -= skill.ManaCost;
    skill.Use();

    var damage = skill.CalculateDamage() + (user.Strength / 2);
    
    if (skill.IsAreaOfEffect)
    {
      var targets = Map.GetAliveMonsters()
        .Where(m => Math.Abs(m.X - target.X) <= skill.AreaRadius && 
                    Math.Abs(m.Y - target.Y) <= skill.AreaRadius)
        .ToList();
      
      foreach (var t in targets)
      {
        t.TakeDamage(damage);
        if (!t.IsAlive)
        {
          ExperienceGained += t.GetExperienceValue();
        }
      }
    }
    else
    {
      target.TakeDamage(damage);
      if (!target.IsAlive)
      {
        ExperienceGained += target.GetExperienceValue();
      }
    }
  }

  private void ProcessMonsterTurns()
  {
    if (Map.Player == null || !Map.Player.IsAlive) return;

    foreach (var monster in Map.GetAliveMonsters())
    {
      Map.Player.TakeDamage(monster.BaseDamage);
    }
  }

  private void CheckBattleEnd()
  {
    if (Map.Player == null || !Map.Player.IsAlive)
    {
      IsFinished = true;
      IsVictory = false;
      return;
    }

    if (Map.IsWaveComplete)
    {
      var mapProfile = _profileService.GetMapProfile(Map.ProfileKey);
      if (mapProfile != null && Map.CurrentWave < Map.TotalWaves)
      {
        Map.CurrentWave++;
        SpawnWave(Map, mapProfile, Map.CurrentWave, Map.Player.Level);
      }
      else
      {
        IsFinished = true;
        IsVictory = true;
        GenerateLoot();
      }
    }
  }

  private void GenerateLoot()
  {
    var itemCount = _lootRandom.Next(1, 4);
    var equipmentProfiles = _profileService.GetAllEquipmentProfiles().ToArray();

    for (int i = 0; i < itemCount; i++)
    {
      var profile = equipmentProfiles[_lootRandom.Next(equipmentProfiles.Length)];

      var item = new ItemDTO
      {
        Id = Guid.NewGuid(),
        ItemType = profile.Key,
        ItemLevel = Map.Player?.Level ?? 1,
        Attributes = new Dictionary<EnumAttribute, int>(profile.BaseAttributes)
      };

      // Add random bonus attributes
      if (_lootRandom.NextDouble() > 0.5)
      {
        var attributes = Enum.GetValues<EnumAttribute>().Where(a => a != EnumAttribute.NotSpecified).ToArray();
        var bonusAttribute = attributes[_lootRandom.Next(attributes.Length)];
        if (!item.Attributes.ContainsKey(bonusAttribute))
        {
          item.Attributes[bonusAttribute] = _lootRandom.Next(1, 5);
        }
        else
        {
          item.Attributes[bonusAttribute] += _lootRandom.Next(1, 3);
        }
      }

      ItemsDropped.Add(item);
    }
  }

  private void UpdateCooldowns()
  {
    if (Map.Player == null) return;

    foreach (var skill in Map.Player.Skills)
    {
      skill.Tick();
    }

    // Regenerate mana
    Map.Player.CurrentMana = Math.Min(Map.Player.MaxMana, Map.Player.CurrentMana + 1);
  }

  /// <summary>
  /// Run the battle to completion
  /// </summary>
  public void RunToCompletion(int maxTicks = 10000)
  {
    while (!IsFinished && CurrentTick < maxTicks)
    {
      ProcessTick();
    }
  }
}
