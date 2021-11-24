using System;
using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class LeastPointsAward : IAward
    {
        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
        {
            var losingTeamResult = fantasyWeekScoreboard
                .FantasyMatchups
                .Select(x => x.Loser)
                .OrderBy(x => x.TotalStarterScore)
                .First();
            var losingFantasyTeam = losingTeamResult
                .FantasyTeam;

            return new AwardWinner
            {
                AwardId = AwardIds.LeastPointsAward,
                AwardText = $"[b]Least Points Scored[/b]{Environment.NewLine}{losingFantasyTeam.TeamName} with {losingTeamResult.TotalStarterScore} points.",
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
                FantasyTeam = losingFantasyTeam,
            };
        }
    }
}