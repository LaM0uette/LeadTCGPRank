using LeadTCGPRank.App.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IStatsHubClient, StatsHubClient>();

await builder.Build().RunAsync();