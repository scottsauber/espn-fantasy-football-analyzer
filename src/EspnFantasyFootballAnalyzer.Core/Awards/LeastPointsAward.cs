using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class LeastPointsAward : IAward
    {
        public AwardWinner AssignAwardToWinner(List<FantasyMatchup> fantasyMatchups, int weekNumber)
        {
            var losingTeamResult = fantasyMatchups
                .Select(x => x.Loser)
                .OrderBy(x => x.TotalStarterScore)
                .First();
            var losingFantasyTeam = losingTeamResult
                .FantasyTeam;
            
            return new AwardWinner
            {
                AwardId = AwardIds.LeastPointsAward,
                AwardText = $"Least Points Scored by {losingFantasyTeam.TeamName} with {losingTeamResult.TotalStarterScore} points.",
                WeekNumber = weekNumber,
                FantasyTeam = losingFantasyTeam,
            };
        }
    }
}