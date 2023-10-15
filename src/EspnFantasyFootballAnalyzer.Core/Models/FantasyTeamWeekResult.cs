namespace EspnFantasyFootballAnalyzer.Core.Models;

public record FantasyTeamWeekResult
{
    public decimal TotalStarterScore => StarterStats.Sum(s => s.Score);
    public decimal TotalBenchScore => BenchStats.Sum(s => s.Score);
    public List<FantasyPlayerWeekStats> StarterStats { get; set; } = new();
    public List<FantasyPlayerWeekStats> BenchStats { get; set; } = new();
    public FantasyTeam FantasyTeam { get; set; }
}