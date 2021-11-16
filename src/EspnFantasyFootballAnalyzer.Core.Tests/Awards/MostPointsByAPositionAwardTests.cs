using System;
using System.Collections;
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
    public class MostPointsByAPositionAwardTests
    {
        private readonly Fixture _fixture;

        public MostPointsByAPositionAwardTests()
        {
            _fixture = new Fixture();
        }

        public class AwardData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MostPointsByAQbStarterAward() };
                yield return new object[] { new MostPointsByARbStarterAward() };
                yield return new object[] { new MostPointsByATightEndStarterAward() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        // Normally I'd break this into separate tests (i.e. the starting position with 3rd most points vs bench of same position with 2nd most points vs another position with the most points)... but the point is given all 3, it should pick the starter 
        // Technically this test doesn't do too much to test the individual awards themselves, just that they implement the interface and don't override something they shouldn't
        [Theory]
        [ClassData(typeof(AwardData))]
        public void ShouldReturnStartAtPositionWithMostPointsWhenAnotherPositionHasMostPointsAndBenchPositionHasMorePointsThanStarter(MostPointsByAPositionAward mostPointsByAPositionAward)
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            var positionWithHighScore = FantasyFactory.CreatePlayerWithPosition(mostPointsByAPositionAward.FantasyPosition);
            var winningTeam = _fixture.Create<FantasyTeam>();
            var winningPositionScore = 20;
            var matchupWithHighOtherPositionScore = FantasyFactory.CreateMatchupWithScores(winningPositionScore, 19, winningTeam, positionWithHighScore);
            matchupWithHighOtherPositionScore.HomeTeam.BenchStats = CreateBenchPositionWithHigherScore(winningPositionScore, mostPointsByAPositionAward.FantasyPosition);
            fantasyMatchups.Add(matchupWithHighOtherPositionScore);
            var anotherPosition = Enum.GetValues<FantasyPosition>()
                .First(x => x != mostPointsByAPositionAward.FantasyPosition);
            var otherMatchup = CreateMatchupWithLowerPositionScoreAndHigherScoreForAnotherPosition(winningPositionScore, mostPointsByAPositionAward.FantasyPosition, anotherPosition);
            fantasyMatchups.Add(otherMatchup);
            var scoreboard = new FantasyWeekScoreboard(fantasyMatchups);
            var weekNumber = 11;

            var result = mostPointsByAPositionAward.AssignAwardToWinner(scoreboard);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(mostPointsByAPositionAward.AwardId);
            var winningPosition = fantasyMatchups
                .SelectMany(x => x.BothTeams)
                .SelectMany(x => x.StarterStats)
                .Single(x => x.FantasyPlayer.Id == positionWithHighScore.Id);
            result.AwardText.Should().Be($"Most Points By A {mostPointsByAPositionAward.FantasyPositionTitle} Starter {winningPosition.FantasyPlayer.FullName} with 20 points from team {winningTeam.TeamName}.");
            result.FantasyTeam.Should().Be(winningTeam);
        }

        private FantasyMatchup CreateMatchupWithLowerPositionScoreAndHigherScoreForAnotherPosition(int winningPositionScore, FantasyPosition positionWeCareAbout, FantasyPosition anotherPosition)
        {
            var positionWithLowerScore = FantasyFactory.CreatePlayerWithPosition(positionWeCareAbout);
            var matchupWithHighIndividualPlayerScore = FantasyFactory.CreateMatchupWithScores(winningPositionScore - 1, winningPositionScore - 1, _fixture.Create<FantasyTeam>(), positionWithLowerScore);
            matchupWithHighIndividualPlayerScore.HomeTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = winningPositionScore + 15,
                FantasyPlayer = new FantasyPlayer
                {
                    Position = anotherPosition,
                }
            });
            return matchupWithHighIndividualPlayerScore;
        }

        private List<FantasyPlayerWeekStats> CreateBenchPositionWithHigherScore(int winningPositionScore,
            FantasyPosition fantasyPosition)
        {
            return new List<FantasyPlayerWeekStats>
            {
                new()
                {
                    FantasyPlayer = new FantasyPlayer
                    {
                        Position = fantasyPosition,
                        Id = _fixture.Create<int>(),
                    },
                    Score = winningPositionScore + 1
                }
            };
        }
    }
}