namespace EspnFantasyFootballAnalyzer.Core.Models;

public record FantasyMatchup
{
    public int WeekNumber { get; set; }
    public FantasyTeamWeekResult HomeTeam { get; init; }
    public FantasyTeamWeekResult AwayTeam { get; init; }
    public List<FantasyTeamWeekResult> BothTeams => new() { HomeTeam, AwayTeam };

    public FantasyTeamWeekResult Winner
    {
        get
        {
            if (HomeTeam.TotalStarterScore != AwayTeam.TotalStarterScore)
            {
                return HomeTeam.TotalStarterScore > AwayTeam.TotalStarterScore ? HomeTeam : AwayTeam;
            }

            if (HomeTeam.TotalBenchScore == AwayTeam.TotalBenchScore)
            {
                // No tie breaker if Starter and Bench scores match
                throw new ArgumentOutOfRangeException();
            }

            return HomeTeam.TotalBenchScore > AwayTeam.TotalBenchScore ? HomeTeam : AwayTeam;
        }
    }

    public FantasyTeamWeekResult Loser => Winner == HomeTeam ? AwayTeam : HomeTeam;
    public decimal PointDifferential => Winner.TotalStarterScore - Loser.TotalStarterScore;
}