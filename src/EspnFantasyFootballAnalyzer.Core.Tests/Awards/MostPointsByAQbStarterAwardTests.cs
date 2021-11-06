using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class MostPointsByAQbStarterAwardTests
    {
        private readonly Fixture _fixture;

        public MostPointsByAQbStarterAwardTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldReturnQbWithMostPointsWhenRunningBackHasMostPointsAndBenchQbHasMorePointsThanStarter()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            var quarterbackPlayerWithHighScore = FantasyFactory.CreatePlayerWithPosition(FantasyPosition.Quarterback);
            var winningTeam = _fixture.Create<FantasyTeam>();
            var winningQbScore = 20;
            var matchupWithHighQbScore = FantasyFactory.CreateMatchupWithScores(winningQbScore, 19, winningTeam, quarterbackPlayerWithHighScore);
            matchupWithHighQbScore.HomeTeam.BenchStats = CreateBenchQbWithHigherScore(winningQbScore);
            fantasyMatchups.Add(matchupWithHighQbScore);
            var otherMatchup = CreateMatchupWithLowerQuarterbackScoreAndHigherRunningBackScore(winningQbScore);
            fantasyMatchups.Add(otherMatchup);
            var scoreboard = new FantasyWeekScoreboard(fantasyMatchups);
            var weekNumber = 11;
            var mostPointsByAQbStarterAward = new MostPointsByAQbStarterAward();

            var result = mostPointsByAQbStarterAward.AssignAwardToWinner(scoreboard);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.MostPointsByAQbStarterAward);
            var winningQb = fantasyMatchups
                .SelectMany(x => x.BothTeams)
                .SelectMany(x => x.StarterStats)
                .Single(x => x.FantasyPlayer.Id == quarterbackPlayerWithHighScore.Id);
            result.AwardText.Should().Be($"Most Points By A QB Starter {winningQb.FantasyPlayer.FullName} with 20 points from team {winningTeam.TeamName}.");
            result.FantasyTeam.Should().Be(winningTeam);
        }

        private FantasyMatchup CreateMatchupWithLowerQuarterbackScoreAndHigherRunningBackScore(int winningQbScore)
        {
            var quarterbackPlayerWithLowScore = FantasyFactory.CreatePlayerWithPosition(FantasyPosition.Quarterback);
            var matchupWithHighIndividualPlayerScore =
                FantasyFactory.CreateMatchupWithScores(winningQbScore - 1, winningQbScore - 1, _fixture.Create<FantasyTeam>(), quarterbackPlayerWithLowScore);
            matchupWithHighIndividualPlayerScore.HomeTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = winningQbScore + 15,
                FantasyPlayer = new FantasyPlayer
                {
                    Position = FantasyPosition.RunningBack,
                }
            });
            return matchupWithHighIndividualPlayerScore;
        }

        private List<FantasyPlayerWeekStats> CreateBenchQbWithHigherScore(int winningQbScore)
        {
            return new List<FantasyPlayerWeekStats>
            {
                new FantasyPlayerWeekStats
                {
                    FantasyPlayer = new FantasyPlayer
                    {
                        Position = FantasyPosition.Quarterback,
                        Id = _fixture.Create<int>(),
                    },
                    Score = winningQbScore + 1
                }
            };
        }
    }
}