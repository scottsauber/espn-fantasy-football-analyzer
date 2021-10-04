using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class MostPointsAwardTests
    {
        [Fact]
        public void ShouldReturnTeamWithTheMostPoints()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            var winningTeamScore = 100;
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(winningTeamScore, 50));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(90, 40));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(20, 10));
            var weekNumber = 11;
            var mostPointsAward = new MostPointsAward();

            var result = mostPointsAward.AssignAwardToWinner(fantasyMatchups, weekNumber);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.MostPointsAward);
            var winningFantasyTeam = fantasyMatchups.Single(x => x.Winner.TotalStarterScore == winningTeamScore).Winner.FantasyTeam;
            result.AwardText.Should().Be($"Most Points Scored by {winningFantasyTeam.TeamName} with {winningTeamScore} points.");
            result.FantasyTeam.Should().Be(winningFantasyTeam);
        }
    }
}