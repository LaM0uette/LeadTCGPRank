using LeadTCGPRank.App.Models;
using LeadTCGPRank.App.Services;
using Microsoft.AspNetCore.SignalR;

namespace LeadTCGPRank.App.Hubs;

public class StatsHub : Hub
{
    #region Statements

    private readonly IStatsService _statsService;

    public StatsHub(IStatsService statsService)
    {
        _statsService = statsService;
    }

    #endregion

    #region Methods
    
    public async Task WinAsync()
    {
        await _statsService.WinAsync();
        await BroadcastAsync();
    }

    public async Task LooseAsync()
    {
        await _statsService.LooseAsync();
        await BroadcastAsync();
    }

    public async Task TieAsync()
    {
        await _statsService.TieAsync();
        await BroadcastAsync();
    }
    

    public async Task<Stats> GetAll()
    {
        return await _statsService.GetAsync();
    }
    
    public async Task SetAll(Stats stats)
    {
        await _statsService.SetAsync(stats);
        await BroadcastAsync();
    }

    
    public async Task<int> GetWins()
    {
        return await _statsService.GetWinsAsync();
    }

    public async Task SetWins(int value)
    {
        await _statsService.SetWinsAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetLooses()
    {
        return await _statsService.GetLoosesAsync();
    }

    public async Task SetLooses(int value)
    {
        await _statsService.SetLoosesAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetTies()
    {
        return await _statsService.GetTiesAsync();
    }

    public async Task SetTies(int value)
    {
        await _statsService.SetTiesAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetPoints()
    {
        return await _statsService.GetPointsAsync();
    }

    public async Task SetPoints(int value)
    {
        await _statsService.SetPointsAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetWinStreaks()
    {
        return await _statsService.GetWinStreaksAsync();
    }

    public async Task SetWinStreaks(int value)
    {
        await _statsService.SetWinStreaksAsync(value);
        await BroadcastAsync();
    }


    private async Task BroadcastAsync()
    {
        Stats stats = await _statsService.GetAsync();
        await Clients.All.SendAsync("StatsUpdated", stats);
    }

    #endregion
}