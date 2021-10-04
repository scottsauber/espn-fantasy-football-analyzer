using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class BiggestBlowoutAwardTests
    {
        [Fact]
        public void ShouldReturnTeamWithBiggestBlowout()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(100, 50));
            var winnerScoreOfBiggestBlowout = 90;
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(winnerScoreOfBiggestBlowout, 30));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(20, 10));
            var weekNumber = 11;
            var biggestBlowoutAward = new BiggestBlowoutAward();

            var result = biggestBlowoutAward.AssignAwardToWinner(fantasyMatchups, weekNumber);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.BiggestBlowoutAward);
            var biggestBlowout = fantasyMatchups.Single(x => x.Winner.TotalStarterScore == winnerScoreOfBiggestBlowout);
            result.AwardText.Should().Be($"Biggest Blowout Award goes to {biggestBlowout.Winner.FantasyTeam.TeamName} for beating {biggestBlowout.Loser.FantasyTeam.TeamName} by 60 points.");
            result.FantasyTeam.Should().Be(biggestBlowout.Winner.FantasyTeam);
        }
    }
}