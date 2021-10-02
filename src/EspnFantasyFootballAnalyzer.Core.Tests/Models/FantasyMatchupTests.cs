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
            var fantasyMatchup = _fixture.Create<FantasyMatchup>();
            fantasyMatchup.HomeTeam.StarterStats.Clear();
            fantasyMatchup.AwayTeam.StarterStats.Clear();
            fantasyMatchup.HomeTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = 100,
            });
            fantasyMatchup.AwayTeam.StarterStats.Add(new FantasyPlayerWeekStats
            {
                Score = 10,
            });

            fantasyMatchup.Winner.Should().Be(fantasyMatchup.HomeTeam);
        }
    }
}