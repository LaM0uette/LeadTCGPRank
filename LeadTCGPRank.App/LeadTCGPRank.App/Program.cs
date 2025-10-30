using LeadTCGPRank.App.Client.Services;
using LeadTCGPRank.App.Components;
using LeadTCGPRank.App.Hubs;
using LeadTCGPRank.App.Services;
using LeadTCGPRank.App.Services.FakeServices;
using LeadTCGPRank.App.Endpoints;
using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();

builder.Services.AddScoped<IStatsService, JsonStatsService>();
builder.Services.AddScoped<IRankService, RankService>();

builder.Services.AddScoped<IStatsHubClient, FakeStatsHubClient>();

WebApplication app = builder.Build();
app.UseForwardedHeaders();

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