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
        int current = await statsService.GetWinsAsync();
        await statsService.SetWinsAsync(current + 1);
        Stats stats = await statsService.GetAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }

    private static async Task<IResult> LoosesAsync(IStatsService statsService, IHubContext<StatsHub> hub)
    {
        int current = await statsService.GetLoosesAsync();
        await statsService.SetLoosesAsync(current + 1);
        Stats stats = await statsService.GetAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }

    private static async Task<IResult> TiesAsync(IStatsService statsService, IHubContext<StatsHub> hub)
    {
        int current = await statsService.GetTiesAsync();
        await statsService.SetTiesAsync(current + 1);
        Stats stats = await statsService.GetAsync();
        await hub.Clients.All.SendAsync("StatsUpdated", stats);
        return Results.Ok(stats);
    }
}