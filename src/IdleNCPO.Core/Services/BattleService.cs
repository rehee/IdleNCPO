using IdleNCPO.Abstractions.Components;
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
  private readonly BattleSeedDTO _seed;
  
  public int BattleSeed { get; }
  public int LootSeed { get; }
  public int CurrentTick { get; private set; }
  public bool IsFinished { get; private set; }
  public bool IsVictory { get; private set; }
  
  public MapIdleComponent Map { get; private set; }
  public int ExperienceGained { get; private set; }
  public List<ItemDTO> ItemsDropped { get; } = new();

  /// <summary>
  /// Maximum ticks for this battle based on map profile
  /// </summary>
  public int MaxTicks { get; private set; }

  public BattleService(ProfileService profileService, BattleSeedDTO seed)
  {
    _profileService = profileService;
    _seed = seed;
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

    // Set MaxTicks based on map profile
    MaxTicks = mapProfile.MaxTicks;

    // Use 20x20 as default size if profile doesn't specify
    var width = mapProfile.Width > 0 ? mapProfile.Width : GameMap2D.DefaultWidth;
    var height = mapProfile.Height > 0 ? mapProfile.Height : GameMap2D.DefaultHeight;

    var map = new MapIdleComponent(seed.MapKey)
    {
      Width = width,
      Height = height,
      CurrentWave = 1,
      TotalWaves = mapProfile.Waves.Count,
      Player = CreatePlayerComponent(seed.Player, width, height)
    };

    // Spawn initial wave
    SpawnWave(map, mapProfile, 1, seed.MapLevel);

    // Initialize the 2D game map
    map.InitializeGameMap();

    return map;
  }

  private CharacterIdleComponent CreatePlayerComponent(CharacterDTO dto, int mapWidth, int mapHeight)
  {
    // Random position for player (typically near spawn area)
    var position = new Position2D(
      _battleRandom.NextDouble() * 3 + 1, // Start in left area (1-4)
      _battleRandom.NextDouble() * (mapHeight - 2) + 1 // Random Y
    );

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
      X = (int)position.X,
      Y = (int)position.Y,
      Position = position,
      MoveSpeed = 0.1
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
        
        // Random 2D position for monster (spawn in right area of map)
        var position = new Position2D(
          _battleRandom.NextDouble() * (map.Width / 2 - 1) + map.Width / 2, // Right half
          _battleRandom.NextDouble() * (map.Height - 2) + 1
        );

        var monster = new MonsterIdleComponent(spawn.MonsterType)
        {
          Name = monsterProfile.Name,
          Level = level,
          MaxHealth = monsterProfile.BaseHealth + (level * 10),
          BaseDamage = monsterProfile.BaseDamage + (level * 2),
          BaseArmor = monsterProfile.BaseArmor + (level / 2),
          BaseExperience = monsterProfile.BaseExperience,
          DamageType = monsterProfile.DamageType,
          X = (int)position.X,
          Y = (int)position.Y,
          Position = position,
          MoveSpeed = 0.08
        };
        monster.CurrentHealth = monster.MaxHealth;
        monster.CurrentMana = monster.MaxMana;
        
        map.Monsters.Add(monster);
      }
    }

    // Update game map actors after spawning new wave
    map.UpdateGameMapActors();
  }

  public void ProcessTick()
  {
    if (IsFinished) return;

    CurrentTick++;

    // Process 2D movement for all actors
    ProcessMovement();

    // Check collisions
    ProcessCollisions();

    // Process player actions
    ProcessPlayerTurn();

    // Process monster actions
    ProcessMonsterTurns();

    // Check win/lose conditions
    CheckBattleEnd();

    // Update cooldowns
    UpdateCooldowns();
  }

  /// <summary>
  /// Process movement for all actors towards their targets
  /// </summary>
  private void ProcessMovement()
  {
    if (Map.GameMap == null) return;

    // Set player's target to nearest alive monster
    if (Map.Player != null && Map.Player.IsAlive)
    {
      var nearestMonster = Map.GetAliveMonsters()
        .OrderBy(m => Map.Player.DistanceTo(m))
        .FirstOrDefault();
      Map.Player.MoveTarget = nearestMonster;
    }

    // Set each monster's target to the player
    foreach (var monster in Map.GetAliveMonsters())
    {
      monster.MoveTarget = Map.Player;
    }

    // Process movement
    Map.GameMap.ProcessMovement();
  }

  /// <summary>
  /// Check and update collision states
  /// </summary>
  private void ProcessCollisions()
  {
    if (Map.GameMap == null) return;
    Map.GameMap.CheckCollisions();
  }

  private void ProcessPlayerTurn()
  {
    if (Map.Player == null || !Map.Player.IsAlive) return;

    var aliveMonsters = Map.GetAliveMonsters();
    if (aliveMonsters.Count == 0) return;

    // Find nearest monster in attack range
    var skill = Map.Player.Skills.FirstOrDefault(s => s.IsReady && Map.Player.CurrentMana >= s.ManaCost);
    if (skill == null) return;

    // Find target in skill range (calculate distance once per monster)
    var target = aliveMonsters
      .Select(m => new { Monster = m, Distance = Map.Player.DistanceTo(m) })
      .Where(x => x.Distance <= skill.Range)
      .OrderBy(x => x.Distance)
      .Select(x => x.Monster)
      .FirstOrDefault();

    if (target != null)
    {
      UseSkill(Map.Player, target, skill);
    }
  }

  private void UseSkill(CharacterIdleComponent user, MonsterIdleComponent target, SkillIdleComponent skill)
  {
    user.CurrentMana -= skill.ManaCost;
    skill.Use();

    var damage = skill.CalculateDamage() + (user.Strength / 2);
    
    if (skill.IsAreaOfEffect && Map.GameMap != null)
    {
      // Use 2D position for area of effect
      var targets = Map.GameMap.GetActorsInRange(target.Position, skill.AreaRadius)
        .OfType<MonsterIdleComponent>()
        .Where(m => m.IsAlive)
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

    // Monsters only attack if they are colliding with the player (close combat)
    foreach (var monster in Map.GetAliveMonsters())
    {
      // Check if monster is in melee range (collision or very close)
      var distance = monster.DistanceTo(Map.Player);
      if (distance <= GameMap2D.CollisionThreshold || monster.IsColliding)
      {
        Map.Player.TakeDamage(monster.BaseDamage);
      }
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
  /// Run the battle to completion using map's MaxTicks
  /// </summary>
  public void RunToCompletion()
  {
    RunToCompletion(MaxTicks);
  }

  /// <summary>
  /// Run the battle to completion with specified max ticks
  /// </summary>
  public void RunToCompletion(int maxTicks)
  {
    while (!IsFinished && CurrentTick < maxTicks)
    {
      ProcessTick();
    }

    // If max ticks reached without victory, mark as defeat
    if (!IsFinished && CurrentTick >= maxTicks)
    {
      IsFinished = true;
      IsVictory = false;
    }
  }

  /// <summary>
  /// Get the battle result DTO for saving and replay
  /// </summary>
  public BattleResultDTO GetBattleResult()
  {
    return new BattleResultDTO
    {
      BattleSeed = BattleSeed,
      LootSeed = LootSeed,
      MapKey = Map.ProfileKey,
      MapLevel = _seed.MapLevel,
      Player = _seed.Player,
      TotalTicks = CurrentTick,
      IsVictory = IsVictory,
      ExperienceGained = ExperienceGained,
      ItemsDropped = new List<ItemDTO>(ItemsDropped),
      CompletedAt = DateTime.UtcNow
    };
  }
}
