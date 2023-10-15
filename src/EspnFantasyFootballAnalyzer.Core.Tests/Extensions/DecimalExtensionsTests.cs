using EspnFantasyFootballAnalyzer.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Extensions;

public class DecimalExtensionsTests
{
    [Fact]
    public void ShouldTruncateAfterTwoDecimalPlaces()
    {
        var value = 100.240000007m;

        var result = value.TruncateAfterTwoDecimalPlaces();

        result.Should().Be(100.24m);
    }
}