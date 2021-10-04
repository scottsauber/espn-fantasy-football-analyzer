using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class BiggestBlowoutAward : IAward
    {
        public AwardWinner AssignAwardToWinner(List<FantasyMatchup> fantasyMatchups, int weekNumber)
        {
            var biggestBlowout = fantasyMatchups
                .OrderByDescending(x => x.PointDifferential)
                .First();
            
            return new AwardWinner
            {
                AwardId = AwardIds.BiggestBlowoutAward,
                AwardText = $"Biggest Blowout Award goes to {biggestBlowout.Winner.FantasyTeam.TeamName} for beating {biggestBlowout.Loser.FantasyTeam.TeamName} by {biggestBlowout.PointDifferential} points.",
                FantasyTeam = biggestBlowout.Winner.FantasyTeam,
                WeekNumber = weekNumber,
            };
        }
    }
}