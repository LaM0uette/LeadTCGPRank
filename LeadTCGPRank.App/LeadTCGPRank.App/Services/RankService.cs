using LeadTCGPRank.App.Models;

namespace LeadTCGPRank.App.Services;

public class RankService : IRankService
{
    #region Statements

    private readonly IStatsService _statsService;

    public RankService(IStatsService statsService)
    {
        _statsService = statsService;
    }

    #endregion

    #region IRankService

    public async Task<Stats> WinAsync(CancellationToken cancellationToken = default)
    {
        int current = await _statsService.GetWinsAsync(cancellationToken);
        await _statsService.SetWinsAsync(current + 1, cancellationToken);
        return await _statsService.GetAsync(cancellationToken);
    }

    public async Task<Stats> LooseAsync(CancellationToken cancellationToken = default)
    {
        int current = await _statsService.GetLoosesAsync(cancellationToken);
        await _statsService.SetLoosesAsync(current + 1, cancellationToken);
        return await _statsService.GetAsync(cancellationToken);
    }

    public async Task<Stats> TieAsync(CancellationToken cancellationToken = default)
    {
        int current = await _statsService.GetTiesAsync(cancellationToken);
        await _statsService.SetTiesAsync(current + 1, cancellationToken);
        return await _statsService.GetAsync(cancellationToken);
    }

    #endregion
}