using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByAQbStarterAward : IAward
    {
        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
        {
            var winningQb = fantasyWeekScoreboard
                .StarterStats
                .Where(x => x.FantasyPlayer.Position == FantasyPosition.Quarterback)
                .OrderByDescending(x => x.Score)
                .First();

            var winningTeam = fantasyWeekScoreboard
                .AllTeams
                .Single(x => x.StarterStats
                                                .Any(y => y.FantasyPlayer.Id 
                                                          == winningQb.FantasyPlayer.Id))
                .FantasyTeam;

            return new AwardWinner
            {
                AwardId = AwardIds.MostPointsByAQbStarterAward,
                AwardText = $"Most Points By A QB Starter {winningQb.FantasyPlayer.FullName} with {winningQb.Score} points from team {winningTeam.TeamName}.",
                FantasyTeam = winningTeam,
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
            };
        }
    }
}