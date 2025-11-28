using IdleNCPO.Core.Services;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Abstractions.Enums;
using IdleNCPO.Core.Helpers;
using IdleNCPO.Abstractions.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProfileService>();
builder.Services.AddSingleton<IBattleServiceFactory<BattleSeedDTO, BattleResultDTO>, BattleServiceFactory>();
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

// API endpoints
app.MapGet("/api/profiles/maps", (ProfileService profileService) =>
{
  return profileService.GetAllMapProfiles();
})
.WithName("GetMaps")
.WithOpenApi();

app.MapGet("/api/profiles/monsters", (ProfileService profileService) =>
{
  return profileService.GetAllMonsterProfiles();
})
.WithName("GetMonsters")
.WithOpenApi();

app.MapGet("/api/profiles/skills", (ProfileService profileService) =>
{
  return profileService.GetAllSkillProfiles();
})
.WithName("GetSkills")
.WithOpenApi();

app.MapGet("/api/profiles/items", (ProfileService profileService) =>
{
  return profileService.GetAllItemProfiles();
})
.WithName("GetItems")
.WithOpenApi();

app.MapGet("/api/profiles/equipment", (ProfileService profileService) =>
{
  return profileService.GetAllEquipmentProfiles();
})
.WithName("GetEquipment")
.WithOpenApi();

app.MapPost("/api/battle/simulate", (IBattleServiceFactory<BattleSeedDTO, BattleResultDTO> battleFactory, BattleSeedDTO seed) =>
{
  var battle = (BattleService)battleFactory.CreateBattle(seed);
  battle.RunToCompletion();
  
  return new
  {
    IsVictory = battle.IsVictory,
    TotalTicks = battle.CurrentTick,
    ExperienceGained = battle.ExperienceGained,
    ItemsDropped = battle.ItemsDropped,
    BattleSeed = battle.BattleSeed,
    LootSeed = battle.LootSeed
  };
})
.WithName("SimulateBattle")
.WithOpenApi();

app.Run();
