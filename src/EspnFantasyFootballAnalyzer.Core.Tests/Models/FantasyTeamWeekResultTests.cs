using System.Collections.Generic;
using EspnFantasyFootballAnalyzer.Core.Models;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Models
{
    public class FantasyTeamWeekResultTests
    {
        [Fact]
        public void TotalStarterScoreShouldBeSumOfAllStarterScores()
        {
            var fantasyTeamWeekResult = new FantasyTeamWeekResult
            {
                StarterStats = new List<FantasyPlayerWeekStats>
                {
                    new FantasyPlayerWeekStats()
                    {
                        Score = 25,
                    },
                    new FantasyPlayerWeekStats()
                    {
                        Score = 35
                    }
                }
            };

            fantasyTeamWeekResult.TotalStarterScore.Should().Be(60);
        }

        [Fact]
        public void TotalBenchScoreShouldBeSumOfAllBenchScores()
        {
            var fantasyTeamWeekResult = new FantasyTeamWeekResult
            {
                BenchStats = new List<FantasyPlayerWeekStats>
                {
                    new FantasyPlayerWeekStats()
                    {
                        Score = 10,
                    },
                    new FantasyPlayerWeekStats()
                    {
                        Score = 15
                    }
                }
            };

            fantasyTeamWeekResult.TotalBenchScore.Should().Be(25);
        }
    }
}