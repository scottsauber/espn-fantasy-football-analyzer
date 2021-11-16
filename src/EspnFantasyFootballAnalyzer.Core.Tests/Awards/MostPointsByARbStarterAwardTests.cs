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
    public class MostPointsByARbStarterAwardTests
    {
         private readonly Fixture _fixture;

         public MostPointsByARbStarterAwardTests()
         {
             _fixture = new Fixture();
         }
        
        [Fact]
        public void ShouldReturnRbWithMostPointsWhenQuarterbackHasMostPointsAndBenchRbHasMorePointsThanStarter()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            var runningBackPlayerWithHighScore = FantasyFactory.CreatePlayerWithPosition(FantasyPosition.RunningBack);
            var winningTeam = _fixture.Create<FantasyTeam>();
            var winningRbScore = 20;
            var matchupWithHighRbScore = FantasyFactory.CreateMatchupWithScores(winningRbScore, 19, winningTeam, runningBackPlayerWithHighScore);
            matchupWithHighRbScore.HomeTeam.BenchStats = CreateBenchQbWithHigherScore(winningRbScore);
            fantasyMatchups.Add(matchupWithHighRbScore);
            var otherMatchup = CreateMatchupWithLowerRunningBackScoreAndHigherQuarterbackScore(winningRbScore);
            fantasyMatchups.Add(otherMatchup);
            var scoreboard = new FantasyWeekScoreboard(fantasyMatchups);
            var weekNumber = 11;
            var mostPointsByAQbStarterAward = new MostPointsByARbStarterAward();

            var result = mostPointsByAQbStarterAward.AssignAwardToWinner(scoreboard);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.MostPointsByARbStarterAward);
            var winningQb = fantasyMatchups
                .SelectMany(x => x.BothTeams)
                .SelectMany(x => x.StarterStats)
                .Single(x => x.FantasyPlayer.Id == runningBackPlayerWithHighScore.Id);
            result.AwardText.Should().Be($"Most Points By A RB Starter {winningQb.FantasyPlayer.FullName} with 20 points from team {winningTeam.TeamName}.");
            result.FantasyTeam.Should().Be(winningTeam);
        }

        private FantasyMatchup CreateMatchupWithLowerRunningBackScoreAndHigherQuarterbackScore(int winningRbScore)
        {
            var runningBackPlayerWithLowScore = FantasyFactory.CreatePlayerWithPosition(FantasyPosition.RunningBack);
            var matchupWithHighIndividualPlayerScore =
                FantasyFactory.CreateMatchupWithScores(winningRbScore - 1, winningRbScore - 1, _fixture.Create<FantasyTeam>(), runningBackPlayerWithLowScore);
            matchupWithHighIndividualPlayerScore.HomeTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = winningRbScore + 15,
                FantasyPlayer = new FantasyPlayer
                {
                    Position = FantasyPosition.Quarterback,
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