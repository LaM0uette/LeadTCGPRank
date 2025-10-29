using LeadTCGPRank.App.Models;
using LeadTCGPRank.App.Services;
using Microsoft.AspNetCore.SignalR;

namespace LeadTCGPRank.App.Hubs;

public class StatsHub : Hub
{
    #region Statements

    private readonly IStatsRepository _repo;

    public StatsHub(IStatsRepository repo)
    {
        _repo = repo;
    }

    #endregion

    #region Methods

    public async Task<Stats> GetAll()
    {
        return await _repo.GetAsync();
    }

    
    public async Task<int> GetWins()
    {
        return await _repo.GetWinsAsync();
    }

    public async Task SetWins(int value)
    {
        await _repo.SetWinsAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetLooses()
    {
        return await _repo.GetLoosesAsync();
    }

    public async Task SetLooses(int value)
    {
        await _repo.SetLoosesAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetTies()
    {
        return await _repo.GetTiesAsync();
    }

    public async Task SetTies(int value)
    {
        await _repo.SetTiesAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetPoints()
    {
        return await _repo.GetPointsAsync();
    }

    public async Task SetPoints(int value)
    {
        await _repo.SetPointsAsync(value);
        await BroadcastAsync();
    }

    
    public async Task<int> GetWinStreaks()
    {
        return await _repo.GetWinStreaksAsync();
    }

    public async Task SetWinStreaks(int value)
    {
        await _repo.SetWinStreaksAsync(value);
        await BroadcastAsync();
    }


    private async Task BroadcastAsync()
    {
        Stats stats = await _repo.GetAsync();
        await Clients.All.SendAsync("StatsUpdated", stats);
    }

    #endregion
}