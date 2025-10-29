﻿using LeadTCGPRank.App.Client.Models;

namespace LeadTCGPRank.App.Client.Services;

public interface IStatsHubClient : IAsyncDisposable
{
    event Action<Stats>? StatsUpdated;

    Task EnsureConnectedAsync();
    
    Task WinAsync();
    Task LooseAsync();
    Task TieAsync();

    Task<Stats> GetAll();
    Task SetAll(Stats stats);

    Task<int> GetWins();
    Task SetWins(int value);

    Task<int> GetLooses();
    Task SetLooses(int value);

    Task<int> GetTies();
    Task SetTies(int value);

    Task<int> GetPoints();
    Task SetPoints(int value);

    Task<int> GetWinStreaks();
    Task SetWinStreaks(int value);
}