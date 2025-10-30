using LeadTCGPRank.App.Client.Services;
using LeadTCGPRank.App.Components;
using LeadTCGPRank.App.Hubs;
using LeadTCGPRank.App.Services;
using LeadTCGPRank.App.Services.FakeServices;
using LeadTCGPRank.App.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();

builder.Services.AddScoped<IStatsService, JsonStatsService>();
builder.Services.AddScoped<IRankService, RankService>();

builder.Services.AddScoped<IStatsHubClient, FakeStatsHubClient>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LeadTCGPRank.App.Client._Imports).Assembly);

app.MapHub<StatsHub>("/hubs/stats");
app.MapStatsEndpoint();

app.Run();