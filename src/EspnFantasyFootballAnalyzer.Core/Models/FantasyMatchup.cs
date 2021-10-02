using System;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyMatchup
    {
        public FantasyTeamWeekResult HomeTeam { get; init; }
        public FantasyTeamWeekResult AwayTeam { get; init; }

        public FantasyTeamWeekResult Winner
        {
            get
            {
                if (HomeTeam.TotalStarterScore != AwayTeam.TotalStarterScore)
                {
                    return HomeTeam.TotalStarterScore > AwayTeam.TotalStarterScore ? HomeTeam : AwayTeam;
                }

                if (HomeTeam.TotalBenchScore == AwayTeam.TotalBenchScore)
                {
                    // No tie breaker if Starter and Bench scores match
                    throw new ArgumentOutOfRangeException();
                }

                return HomeTeam.TotalBenchScore > AwayTeam.TotalBenchScore ? HomeTeam : AwayTeam;
            }
        }
    }
}