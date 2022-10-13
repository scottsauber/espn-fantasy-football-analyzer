using System;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards;

public class MostPointsInLossAward : IAward
{
    public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
    {
        var mostPointsInLoss = fantasyWeekScoreboard
            .FantasyMatchups
            .OrderByDescending(x => x.Loser.TotalStarterScore)
            .First();
        
        return new AwardWinner
        {
            AwardId = AwardIds.MostPointsInLossAward,
            AwardText = $"[b]Most Points In Loss[/b]${Environment.NewLine}{mostPointsInLoss.Loser.FantasyTeam.TeamName} for scoring {mostPointsInLoss.Loser.TotalStarterScore} points in a loss.",
            FantasyTeam = mostPointsInLoss.Loser.FantasyTeam,
            WeekNumber = fantasyWeekScoreboard.WeekNumber,
        };
    }
}