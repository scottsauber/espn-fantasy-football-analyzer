using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards;

public class MostPointsAward : IAward
{
    public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
    {
        var winningTeamResult = fantasyWeekScoreboard
            .FantasyMatchups
            .Select(x => x.Winner)
            .OrderByDescending(x => x.TotalStarterScore)
            .First();
        var winningFantasyTeam = winningTeamResult
            .FantasyTeam;

        return new AwardWinner
        {
            AwardId = AwardIds.MostPointsAward,
            AwardText = $"[b]Most Points Scored[/b]{Environment.NewLine}{winningFantasyTeam.TeamName} with {winningTeamResult.TotalStarterScore} points.",
            WeekNumber = fantasyWeekScoreboard.WeekNumber,
            FantasyTeam = winningFantasyTeam,
        };
    }
}