namespace LeadTCGPRank.App.Models;

public class Stats
{
    public int Wins { get; set; }
    public int Looses { get; set; }
    public int Ties { get; set; }
    public int Points { get; set; }
    public int WinStreaks { get; set; }

    // Computed rank information based on points
    public Rank Rank => Rankings.GetRankForPoints(Points);
    public string RankLabel => Rankings.GetLabel(Rank);
}