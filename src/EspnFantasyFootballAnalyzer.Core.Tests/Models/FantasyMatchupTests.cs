using System;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Models;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Models
{
    public class FantasyMatchupTests
    {
        private readonly Fixture _fixture;

        public FantasyMatchupTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldReturnWinnerBasedOnMostPoints()
        {
            var fantasyMatchup = CreateFantasyMatchup(100, 10);

            fantasyMatchup.Winner.Should().Be(fantasyMatchup.HomeTeam);
        }
        
        [Fact]
        public void ShouldReturnWinnerBasedOnMostBenchPointsWhenStarterScoreMatches()
        {
            var starterScore = 100;
            var fantasyMatchup = CreateFantasyMatchup(starterScore, starterScore, 100, 10);

            fantasyMatchup.Winner.Should().Be(fantasyMatchup.HomeTeam);
        }
        
        [Fact]
        public void ShouldThrowExceptionWhenStarterPointsAndBenchPointsMatch()
        {
            var matchingScore = 100;
            var fantasyMatchup = CreateFantasyMatchup(matchingScore, matchingScore, matchingScore, matchingScore);

            FluentActions.Invoking(() => fantasyMatchup.Winner).Should().Throw<ArgumentOutOfRangeException>();
        }

        private FantasyMatchup CreateFantasyMatchup(int homeTeamStarterScore, int awayTeamStarterScore, int homeTeamBenchScore = 0, int awayTeamBenchScore = 0)
        {
            var fantasyMatchup = _fixture.Create<FantasyMatchup>();

            fantasyMatchup.HomeTeam.StarterStats.Clear();
            fantasyMatchup.HomeTeam.BenchStats.Clear();
            fantasyMatchup.AwayTeam.StarterStats.Clear();
            fantasyMatchup.AwayTeam.BenchStats.Clear();
            fantasyMatchup.HomeTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = homeTeamStarterScore,
            });
            fantasyMatchup.AwayTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = awayTeamStarterScore,
            });
            fantasyMatchup.HomeTeam.BenchStats.Add(new FantasyPlayerWeekStats
            {
                Score = homeTeamBenchScore,
            });
            fantasyMatchup.AwayTeam.BenchStats.Add(new FantasyPlayerWeekStats
            {
                Score = awayTeamBenchScore,
            });
            return fantasyMatchup;
        }
    }
}