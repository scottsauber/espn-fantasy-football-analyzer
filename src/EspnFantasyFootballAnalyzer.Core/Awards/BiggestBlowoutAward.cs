using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class BiggestBlowoutAward : IAward
    {
        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
        {
            var biggestBlowout = fantasyWeekScoreboard
                .FantasyMatchups
                .OrderByDescending(x => x.PointDifferential)
                .First();
            
            return new AwardWinner
            {
                AwardId = AwardIds.BiggestBlowoutAward,
                AwardText = $"Biggest Blowout Award goes to {biggestBlowout.Winner.FantasyTeam.TeamName} for beating {biggestBlowout.Loser.FantasyTeam.TeamName} by {biggestBlowout.PointDifferential} points.",
                FantasyTeam = biggestBlowout.Winner.FantasyTeam,
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
            };
        }
    }
}