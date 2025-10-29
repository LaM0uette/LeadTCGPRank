using LeadTCGPRank.App.Client.Models;
using LeadTCGPRank.App.Client.Services;

namespace LeadTCGPRank.App.Services.FakeServices;

public class FakeStatsHubClient : IStatsHubClient
{
    public event Action<Stats>? StatsUpdated;
    
    // No-op connect: never throws, does nothing.
    public Task EnsureConnectedAsync() => Task.CompletedTask;

    // Return default/empty stats, do not emit events.
    public Task<Stats> GetAll() => Task.FromResult(new Stats());

    public Task<int> GetWins() => Task.FromResult(0);
    public Task SetWins(int value) => Task.CompletedTask;

    public Task<int> GetLooses() => Task.FromResult(0);
    public Task SetLooses(int value) => Task.CompletedTask;

    public Task<int> GetTies() => Task.FromResult(0);
    public Task SetTies(int value) => Task.CompletedTask;

    public Task<int> GetPoints() => Task.FromResult(0);
    public Task SetPoints(int value) => Task.CompletedTask;

    public Task<int> GetWinStreaks() => Task.FromResult(0);
    public Task SetWinStreaks(int value) => Task.CompletedTask;
    
    public ValueTask DisposeAsync()
    {
        // Nothing to dispose; ensure no exceptions.
        return ValueTask.CompletedTask;
        GC.SuppressFinalize(this);
    }
}