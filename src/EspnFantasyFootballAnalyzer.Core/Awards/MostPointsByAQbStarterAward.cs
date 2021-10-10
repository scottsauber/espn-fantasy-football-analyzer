using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByAQbStarterAward : IAward
    {
        public AwardWinner AssignAwardToWinner(List<FantasyMatchup> fantasyMatchups, int weekNumber)
        {
            var winningQb = fantasyMatchups
                .SelectMany(x => x.BothTeams)
                .SelectMany(x => x.StarterStats)
                .Where(x => x.FantasyPlayer.Position == FantasyPosition.Quarterback)
                .OrderByDescending(x => x.Score)
                .First();

            var winningTeam = fantasyMatchups
                .SelectMany(x => x.BothTeams)
                .Single(x => x.StarterStats
                                                .Any(x => x.FantasyPlayer.Id 
                                                          == winningQb.FantasyPlayer.Id))
                .FantasyTeam;

            return new AwardWinner
            {
                AwardId = AwardIds.MostPointsByAQbStarterAward,
                AwardText = $"Most Points By A QB Starter {winningQb.FantasyPlayer.FullName} with {winningQb.Score} points from team {winningTeam.TeamName}.",
                FantasyTeam = winningTeam,
                WeekNumber = weekNumber,
            };
        }
    }
}