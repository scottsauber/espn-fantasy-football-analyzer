using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards;

public class SmallestMarginOfVictoryAward : IAward
{
    public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
    {
        var smallestMargin = fantasyWeekScoreboard
            .FantasyMatchups
            .OrderBy(x => x.PointDifferential)
            .First();

        return new AwardWinner
        {
            AwardId = AwardIds.SmallestMarginOfVictoryAward,
            AwardText = $"[b]Smallest Margin of Victory[/b]{Environment.NewLine}{smallestMargin.Winner.FantasyTeam.TeamName} for beating {smallestMargin.Loser.FantasyTeam.TeamName} by {smallestMargin.PointDifferential} points.",
            FantasyTeam = smallestMargin.Winner.FantasyTeam,
            WeekNumber = fantasyWeekScoreboard.WeekNumber,
        };
    }
}