using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class MostPointsAwardTests
    {
        private readonly Fixture _fixture;

        public MostPointsAwardTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldReturnTeamWithTheMostPoints()
        {
            var fantasyScoreboard = _fixture.Create<FantasyWeekScoreboard>();
            fantasyScoreboard.FantasyMatchups.Clear();
            var winningTeamScore = 100;
            fantasyScoreboard.FantasyMatchups.Add(CreateMatchupWithScores(winningTeamScore, 50));
            fantasyScoreboard.FantasyMatchups.Add(CreateMatchupWithScores(90, 40));
            fantasyScoreboard.FantasyMatchups.Add(CreateMatchupWithScores(20, 10));
            var mostPointsAward = new MostPointsAward();

            var result = mostPointsAward.AssignAwardToWinner(fantasyScoreboard);

            result.WeekNumber.Should().Be(fantasyScoreboard.WeekNumber);
            var winningFantasyTeam = fantasyScoreboard.FantasyMatchups.Single(x => x.Winner.TotalStarterScore == winningTeamScore).Winner.FantasyTeam;
            result.FantasyTeam.Should().Be(winningFantasyTeam);
        }

        private FantasyMatchup CreateMatchupWithScores(int homeTeamScore, int awayTeamScore)
        {
            return new FantasyMatchup
            {
                HomeTeam = CreateTeamResultWithScore(homeTeamScore),
                AwayTeam = CreateTeamResultWithScore(awayTeamScore),
            };
        }

        private FantasyTeamWeekResult CreateTeamResultWithScore(int score)
        {
            return new FantasyTeamWeekResult
            {
                FantasyTeam = _fixture.Create<FantasyTeam>(),
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