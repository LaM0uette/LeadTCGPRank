using LeadTCGPRank.App.Hubs;
using LeadTCGPRank.App.Models;
using LeadTCGPRank.App.Services;
using Microsoft.AspNetCore.SignalR;

namespace LeadTCGPRank.App.Api;

public static class StatsEndpoint
{
    public static IEndpointRouteBuilder MapStatsEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/stats");

        group.MapPost("/win", WinsAsync);
        group.MapPost("/loose", LoosesAsync);
        group.MapPost("/tie", TiesAsync);

        return app;
    }

    private static async Task<IResult> WinsAsync(IStatsService statsService, IHubContext<StatsHub> hub)
    {
        Stats stats = await statsService.WinAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }

    private static async Task<IResult> LoosesAsync(IStatsService statsService, IHubContext<StatsHub> hub)
    {
        Stats stats = await statsService.LooseAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }

    private static async Task<IResult> TiesAsync(IStatsService statsService, IHubContext<StatsHub> hub)
    {
        Stats stats = await statsService.TieAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }
}