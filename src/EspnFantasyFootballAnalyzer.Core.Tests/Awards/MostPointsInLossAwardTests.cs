using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards;

public class MostPointsInLossAwardTests
{
    [Fact]
    public void ShouldReturnTeamWithMostPointsInLoss()
    {
        var fantasyMatchups = new List<FantasyMatchup>();
        var mostPointsInLoss = 90;
        var mostPointsInLossMatchup = FantasyFactory.CreateMatchupWithScores(mostPointsInLoss + 1, mostPointsInLoss);
        fantasyMatchups.Add(mostPointsInLossMatchup);
        var leastPointsInLossMatchup = FantasyFactory.CreateMatchupWithScores(mostPointsInLoss, mostPointsInLoss - 1);
        fantasyMatchups.Add(leastPointsInLossMatchup);
        var scoreboard = new FantasyWeekScoreboard(fantasyMatchups);
        var mostPointsInLossAward = new MostPointsInLossAward();

        var result = mostPointsInLossAward.AssignAwardToWinner(scoreboard);

        result.WeekNumber.Should().Be(scoreboard.WeekNumber);
        result.AwardId.Should().Be(AwardIds.MostPointsInLossAward);
        var mostPointsInLossAwardWinner = fantasyMatchups.Single(x => x.Loser.TotalStarterScore == mostPointsInLoss);
        result.AwardText.Should().Be($"[b]Most Points In Loss[/b]{Environment.NewLine}{mostPointsInLossAwardWinner.Loser.FantasyTeam.TeamName} for scoring {mostPointsInLossAwardWinner.Loser.TotalStarterScore} points in a loss.");
        result.FantasyTeam.Should().Be(mostPointsInLossAwardWinner.Loser.FantasyTeam);
    }
}