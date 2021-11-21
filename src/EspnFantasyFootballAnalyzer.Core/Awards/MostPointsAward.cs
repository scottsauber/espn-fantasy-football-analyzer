using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
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
                AwardText = $"Most Points Scored by {winningFantasyTeam.TeamName} with {winningTeamResult.TotalStarterScore} points.",
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
                FantasyTeam = winningFantasyTeam,
            };
        }
    }
}