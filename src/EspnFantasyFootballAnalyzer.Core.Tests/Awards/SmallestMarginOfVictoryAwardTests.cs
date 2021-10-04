using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class SmallestMarginOfVictoryAwardTests
    {
        [Fact]
        public void ShouldReturnTeamWithSmallestMarginOfVictory()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(100, 50));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(90, 30));
            var winnerScoreOfSmallestMargin = 20;
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(winnerScoreOfSmallestMargin, 10));
            var weekNumber = 11;
            var smallestMarginOfVictoryAward = new SmallestMarginOfVictoryAward();

            var result = smallestMarginOfVictoryAward.AssignAwardToWinner(fantasyMatchups, weekNumber);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.SmallestMarginOfVictoryAward);
            var smallestMargin = fantasyMatchups.Single(x => x.Winner.TotalStarterScore == winnerScoreOfSmallestMargin);
            result.AwardText.Should().Be($"Smallest Margin of Victory Award goes to {smallestMargin.Winner.FantasyTeam.TeamName} for beating {smallestMargin.Loser.FantasyTeam.TeamName} by 10 points.");
            result.FantasyTeam.Should().Be(smallestMargin.Winner.FantasyTeam);
        }

    }
}