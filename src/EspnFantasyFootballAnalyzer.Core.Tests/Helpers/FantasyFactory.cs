using System.Collections.Generic;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Helpers
{
    public static class FantasyFactory
    {
        public static FantasyMatchup CreateMatchupWithScores(int homeTeamScore, int awayTeamScore)
        {
            return new FantasyMatchup
            {
                HomeTeam = CreateTeamResultWithScore(homeTeamScore),
                AwayTeam = CreateTeamResultWithScore(awayTeamScore),
            };
        }

        public static FantasyTeamWeekResult CreateTeamResultWithScore(int score)
        {
            return new FantasyTeamWeekResult
            {
                FantasyTeam = new Fixture().Create<FantasyTeam>(),
                StarterStats = new List<FantasyPlayerWeekStats>
                {
                    new FantasyPlayerWeekStats
                    {
                        Score = score,
                    }
                }
            };
        }
    }
}