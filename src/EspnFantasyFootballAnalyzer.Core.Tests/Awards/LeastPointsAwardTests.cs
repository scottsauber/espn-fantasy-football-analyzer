using System;
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
    public class LeastPointsAwardTests
    {
        [Fact]
        public void ShouldReturnTeamWithTheLeastPoints()
        {
            var fantasyMatchups = new List<FantasyMatchup>();
            var leastPointsScore = 10;
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(100, 50));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(90, 40));
            fantasyMatchups.Add(FantasyFactory.CreateMatchupWithScores(20, leastPointsScore));
            var scoreboard = new FantasyWeekScoreboard(fantasyMatchups);
            var weekNumber = 11;
            var leastPointsAward = new LeastPointsAward();

            var result = leastPointsAward.AssignAwardToWinner(scoreboard);

            result.WeekNumber.Should().Be(weekNumber);
            result.AwardId.Should().Be(AwardIds.LeastPointsAward);
            var losingFantasyTeam = fantasyMatchups.Single(x => x.Loser.TotalStarterScore == leastPointsScore).Loser.FantasyTeam;
            result.AwardText.Should().Be($"[b]Least Points Scored[/b]{Environment.NewLine}{losingFantasyTeam.TeamName} with {leastPointsScore} points.");
            result.FantasyTeam.Should().Be(losingFantasyTeam);
        }
    }
}