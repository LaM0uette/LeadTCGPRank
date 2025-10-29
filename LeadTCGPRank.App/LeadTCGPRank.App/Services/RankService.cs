using LeadTCGPRank.App.Models;

namespace LeadTCGPRank.App.Services;

public class RankService : IRankService
{
    #region Statements
    
    private const int _baseWinPoints = 10;
    private readonly Dictionary<int, int> _winStreaksBonusPoints = new() { { 0, 0 }, { 1, 0 }, { 2, 3 }, { 3, 6 }, { 4, 9 }, { 5, 12 } };

    private readonly IStatsService _statsService;

    public RankService(IStatsService statsService)
    {
        _statsService = statsService;
    }

    #endregion

    #region IRankService

    public async Task<Stats> WinAsync(CancellationToken cancellationToken = default)
    {
        // Update Wins
        int currentWins = await _statsService.GetWinsAsync(cancellationToken);
        currentWins++;
        
        await _statsService.SetWinsAsync(currentWins, cancellationToken);
        
        // Update Win Streaks
        int currentWinStreaks = await _statsService.GetWinStreaksAsync(cancellationToken);
        currentWinStreaks++;
        
        await _statsService.SetWinStreaksAsync(currentWinStreaks, cancellationToken);
        
        // Update Points
        int currentPoints = await _statsService.GetPointsAsync(cancellationToken);
        currentPoints += _baseWinPoints;
        
        if (_winStreaksBonusPoints.TryGetValue(currentWinStreaks, out int point))
        {
            currentPoints += point;
        }
        else if (currentWinStreaks > 5)
        {
            currentPoints += _winStreaksBonusPoints[5];
        }
        
        await _statsService.SetPointsAsync(currentPoints, cancellationToken);
        
        return await _statsService.GetAsync(cancellationToken);
    }

    public async Task<Stats> LooseAsync(CancellationToken cancellationToken = default)
    {
        // Update Losses
        int currentLosses = await _statsService.GetLoosesAsync(cancellationToken);
        currentLosses++;
        
        await _statsService.SetLoosesAsync(currentLosses, cancellationToken);
        
        // Reset Win Streaks
        await _statsService.SetWinStreaksAsync(0, cancellationToken);
        
        // Update Points
        int currentPoints = await _statsService.GetPointsAsync(cancellationToken);
        
        
        return await _statsService.GetAsync(cancellationToken);
    }

    public async Task<Stats> TieAsync(CancellationToken cancellationToken = default)
    {
        // Update Ties
        int currentTies = await _statsService.GetTiesAsync(cancellationToken);
        currentTies++;
        
        await _statsService.SetTiesAsync(currentTies, cancellationToken);
        
        // Reset Win Streaks
        await _statsService.SetWinStreaksAsync(0, cancellationToken);
        
        return await _statsService.GetAsync(cancellationToken);
    }

    #endregion
}