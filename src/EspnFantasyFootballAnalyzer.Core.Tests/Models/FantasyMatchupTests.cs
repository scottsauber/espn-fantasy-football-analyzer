using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Models;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Models;

public class FantasyMatchupTests
{
    private readonly Fixture _fixture;

    public FantasyMatchupTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void WinnerShouldReturnHomeTeamWhenHomeTeamHasMostStarterPoints()
    {
        var fantasyMatchup = CreateFantasyMatchup(100, 10);

        fantasyMatchup.Winner.Should().Be(fantasyMatchup.HomeTeam);
    }

    [Fact]
    public void LoserShouldReturnAwayTeamWhenHomeTeamHasMostStarterPoints()
    {
        var fantasyMatchup = CreateFantasyMatchup(100, 10);

        fantasyMatchup.Loser.Should().Be(fantasyMatchup.AwayTeam);
    }

    [Fact]
    public void WinnerShouldReturnHomeTeamWhenHomeAndAwayTeamHaveTiedStarterPointsButHomeTeamHasMoreBenchPoints()
    {
        var starterScore = 100;
        var fantasyMatchup = CreateFantasyMatchup(starterScore, starterScore, 100, 10);

        fantasyMatchup.Winner.Should().Be(fantasyMatchup.HomeTeam);
    }

    [Fact]
    public void LoserShouldReturnAwayTeamWhenHomeAndAwayTeamHaveTiedStarterPointsButHomeTeamHasMoreBenchPoints()
    {
        var starterScore = 100;
        var fantasyMatchup = CreateFantasyMatchup(starterScore, starterScore, 100, 10);

        fantasyMatchup.Loser.Should().Be(fantasyMatchup.AwayTeam);
    }

    [Fact]
    public void WinnerShouldThrowExceptionWhenStarterPointsAndBenchPointsMatchForHomeAndAwayTeam()
    {
        var matchingScore = 100;
        var fantasyMatchup = CreateFantasyMatchup(matchingScore, matchingScore, matchingScore, matchingScore);

        FluentActions.Invoking(() => fantasyMatchup.Winner).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void PointDifferentialShouldBeWinningScoreMinusLosingScore()
    {
        var fantasyMatchup = CreateFantasyMatchup(100, 40);

        fantasyMatchup.PointDifferential.Should().Be(60);
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