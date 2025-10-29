using LeadTCGPRank.App.Client.Services;
using LeadTCGPRank.App.Components;
using LeadTCGPRank.App.Hubs;
using LeadTCGPRank.App.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddScoped<StatsHubClient>();
builder.Services.AddSingleton<IStatsRepository, JsonStatsRepository>();

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

app.Run();