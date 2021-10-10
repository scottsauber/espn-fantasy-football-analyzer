using EspnFantasyFootballAnalyzer.Core.Models;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Models
{
    public class FantasyPlayerTests
    {
        [Fact]
        public void ShouldReturnFullNameBasedOnFirstAndLastName()
        {
            var player = new FantasyPlayer
            {
                FirstName = "SpongeBob",
                LastName = "Squarepants"
            };

            player.FullName.Should().Be("SpongeBob Squarepants");
        }
    }
}