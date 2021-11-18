using System;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public abstract class MostPointsByAPositionAward : IAward
    {
        public abstract Guid AwardId { get; }
        public abstract FantasyPosition FantasyPosition { get; }
        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
        {
            var winningPosition = fantasyWeekScoreboard
                .StarterStats
                .Where(x => x.FantasyPlayer.Position == FantasyPosition)
                .OrderByDescending(x => x.Score)
                .First();

            var winningTeam = fantasyWeekScoreboard
                .AllTeams
                .Single(x => x.StarterStats
                    .Any(y => y.FantasyPlayer.Id 
                              == winningPosition.FantasyPlayer.Id))
                .FantasyTeam;

            return new AwardWinner
            {
                AwardId = AwardId,
                AwardText = $"Most Points By A {FantasyPosition.PositionName} Starter {winningPosition.FantasyPlayer.FullName} with {winningPosition.Score} points from team {winningTeam.TeamName}.",
                FantasyTeam = winningTeam,
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
            };
        }
    }
}