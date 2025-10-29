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
    
    [Inject] private StatsHubClient _hub { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _hub.StatsUpdated += OnStatsUpdated;
        await _hub.EnsureConnectedAsync();
        Stats s = await _hub.GetAll();
        ApplyStats(s);
    }

    #endregion

    #region Methods

    protected async Task SetWins(int value)
    {
        await _hub.SetWins(value);
    }

    protected async Task SetLooses(int value)
    {
        await _hub.SetLooses(value);
    }

    protected async Task SetTies(int value)
    {
        await _hub.SetTies(value);
    }

    protected async Task SetPoints(int value)
    {
        await _hub.SetPoints(value);
    }

    protected async Task SetWinStreaks(int value)
    {
        await _hub.SetWinStreaks(value);
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
        _hub.StatsUpdated -= OnStatsUpdated;
        GC.SuppressFinalize(this);
    }

    #endregion
}