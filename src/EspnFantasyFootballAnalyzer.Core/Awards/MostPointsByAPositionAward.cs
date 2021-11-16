using System;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public abstract class MostPointsByAPositionAward : IAward
    {
        public abstract Guid AwardId { get; }
        public abstract FantasyPosition FantasyPosition { get; }
        public abstract string FantasyPositionTitle { get; }
        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard)
        {
            var winningPosiiton = fantasyWeekScoreboard
                .StarterStats
                .Where(x => x.FantasyPlayer.Position == FantasyPosition)
                .OrderByDescending(x => x.Score)
                .First();

            var winningTeam = fantasyWeekScoreboard
                .AllTeams
                .Single(x => x.StarterStats
                    .Any(y => y.FantasyPlayer.Id 
                              == winningPosiiton.FantasyPlayer.Id))
                .FantasyTeam;

            return new AwardWinner
            {
                AwardId = AwardId,
                AwardText = $"Most Points By A {FantasyPositionTitle} Starter {winningPosiiton.FantasyPlayer.FullName} with {winningPosiiton.Score} points from team {winningTeam.TeamName}.",
                FantasyTeam = winningTeam,
                WeekNumber = fantasyWeekScoreboard.WeekNumber,
            };
        }
    }
}