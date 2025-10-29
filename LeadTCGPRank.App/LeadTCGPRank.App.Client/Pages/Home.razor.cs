using LeadTCGPRank.App.Client.Models;
using LeadTCGPRank.App.Client.Services;
using Microsoft.AspNetCore.Components;

namespace LeadTCGPRank.App.Client.Pages;

public class HomeBase : ComponentBase, IDisposable
{
    #region Statements

    protected  Stats? Stats;
    protected  int Wins;
    protected  int Looses;
    protected  int Ties;
    protected  int Points;
    protected  int WinStreaks;
    
    [Inject] private IStatsHubClient _statsHubClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _statsHubClient.StatsUpdated += OnStatsUpdated;
        await _statsHubClient.EnsureConnectedAsync();
        Stats s = await _statsHubClient.GetAll();
        ApplyStats(s);
    }

    #endregion

    #region Methods

    protected async Task SetWins(int value)
    {
        await _statsHubClient.SetWins(value);
    }

    protected async Task SetLooses(int value)
    {
        await _statsHubClient.SetLooses(value);
    }

    protected async Task SetTies(int value)
    {
        await _statsHubClient.SetTies(value);
    }

    protected async Task SetPoints(int value)
    {
        await _statsHubClient.SetPoints(value);
    }

    protected async Task SetWinStreaks(int value)
    {
        await _statsHubClient.SetWinStreaks(value);
    }
    
    
    private void OnStatsUpdated(Stats s)
    {
        ApplyStats(s);
        InvokeAsync(StateHasChanged);
    }

    private void ApplyStats(Stats s)
    {
        Stats = s;
        Wins = s.Wins;
        Looses = s.Looses;
        Ties = s.Ties;
        Points = s.Points;
        WinStreaks = s.WinStreaks;
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
        _statsHubClient.StatsUpdated -= OnStatsUpdated;
        GC.SuppressFinalize(this);
    }

    #endregion
}