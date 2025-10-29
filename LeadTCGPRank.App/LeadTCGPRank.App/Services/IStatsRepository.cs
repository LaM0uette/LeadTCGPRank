using LeadTCGPRank.App.Models;

namespace LeadTCGPRank.App.Services;

public interface IStatsRepository
{
    Task<Stats> GetAsync(CancellationToken cancellationToken = default);
    Task SetAsync(Stats stats, CancellationToken cancellationToken = default);
    
    Task<int> GetWinsAsync(CancellationToken cancellationToken = default);
    Task SetWinsAsync(int value, CancellationToken cancellationToken = default);
    
    Task<int> GetLoosesAsync(CancellationToken cancellationToken = default);
    Task SetLoosesAsync(int value, CancellationToken cancellationToken = default);
    
    Task<int> GetTiesAsync(CancellationToken cancellationToken = default);
    Task SetTiesAsync(int value, CancellationToken cancellationToken = default);
    
    Task<int> GetPointsAsync(CancellationToken cancellationToken = default);
    Task SetPointsAsync(int value, CancellationToken cancellationToken = default);
    
    Task<int> GetWinStreaksAsync(CancellationToken cancellationToken = default);
    Task SetWinStreaksAsync(int value, CancellationToken cancellationToken = default);
}