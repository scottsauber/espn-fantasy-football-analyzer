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
            var fantasyMatchups = new List<FantasyMatchup>();
            var winningTeamScore = 100;
            fantasyMatchups.Add(CreateMatchupWithScores(winningTeamScore, 50));
            fantasyMatchups.Add(CreateMatchupWithScores(90, 40));
            fantasyMatchups.Add(CreateMatchupWithScores(20, 10));
            var weekNumber = 11;
            var mostPointsAward = new MostPointsAward();

            var result = mostPointsAward.AssignAwardToWinner(fantasyMatchups, weekNumber);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.MostPointsAward);
            var winningFantasyTeam = fantasyMatchups.Single(x => x.Winner.TotalStarterScore == winningTeamScore).Winner.FantasyTeam;
            result.AwardText.Should().Be($"Most Points Scored by {winningFantasyTeam.TeamName} with {winningTeamScore} points.");
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