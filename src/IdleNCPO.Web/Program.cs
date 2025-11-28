using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IdleNCPO.Web;
using IdleNCPO.Core.Services;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Abstractions.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ProfileService>();
builder.Services.AddSingleton<IProfileService>(sp => sp.GetRequiredService<ProfileService>());
builder.Services.AddSingleton<IBattleServiceFactory<BattleSeedDTO, BattleResultDTO>, BattleServiceFactory>();

await builder.Build().RunAsync();
