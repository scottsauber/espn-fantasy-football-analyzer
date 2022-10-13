using System.Net;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.EspnData;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class AwardServiceTests
    {
        private readonly AwardService _awardService;

        public AwardServiceTests()
        {
            var testJson = File.ReadAllText(Path.Join("Awards", "test.json"));
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testJson)
                });
            var httpClient = new HttpClient(mockMessageHandler.Object);
            _awardService = new AwardService(new EspnDataMapperService(), httpClient);
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsScoredTeam()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var mostPointsAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsAward);
            mostPointsAward.AwardText.Should().Be($"[b]Most Points Scored[/b]{Environment.NewLine}Waveland Woods Packers with 164.24 points.");
        }

        [Fact]
        public async Task ShouldReturnCorrectLeastPointsScoreTeam()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var mostPointsAward = awardWinners.Single(x => x.AwardId == AwardIds.LeastPointsAward);
            mostPointsAward.AwardText.Should().Be($"[b]Least Points Scored[/b]{Environment.NewLine}Team X-Bladz with 61.42 points.");
        }

        [Fact]
        public async Task ShouldReturnCorrectBlowoutTeam()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.BiggestBlowoutAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Biggest Blowout[/b]{Environment.NewLine}Azeroth High Warlord for beating Team X-Bladz by 49.00 points.");
        }

        [Fact]
        public async Task ShouldReturnCorrectSmallestMarginOfVictoryTeam()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.SmallestMarginOfVictoryAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Smallest Margin of Victory[/b]{Environment.NewLine}Wanta Fant-a? !? for beating Pullen My Pickle by 2.48 points.");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsByAQbStarterAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsByAQbStarterAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Most Points By A Quarterback Starter[/b]{Environment.NewLine}Josh Allen with 37.22 points from team Broadway St Hootinannies .");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsByARbStarterAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsByARbStarterAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Most Points By A Running Back Starter[/b]{Environment.NewLine}Najee Harris with 28.2 points from team Sam's Town Killers.");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsByAWideReceiverStarterAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsByAWrStarterAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Most Points By A Wide Receiver Starter[/b]{Environment.NewLine}Mike Williams with 33.2 points from team Taco Bell.");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsByATightEndStarterAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsByATightEndStarterAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Most Points By A Tight End Starter[/b]{Environment.NewLine}Travis Kelce with 17.4 points from team Purdy Bad.");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsByADefenseSpecialTeamsStarterAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var biggestBlowoutAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsByADefenseSpecialTeamsAward);
            biggestBlowoutAward.AwardText.Should().Be($"[b]Most Points By A Defense/Special Teams Starter[/b]{Environment.NewLine}Saints D/ST with 19 points from team Wanta Fant-a? !?.");
        }

        [Fact]
        public async Task ShouldReturnCorrectMostPointsInLossAward()
        {
            var awardWinners = await _awardService.GetAwardWinnersForWeekAsync(2021, 3);
            
            var mostPointsInLossAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsInLossAward);
            mostPointsInLossAward.AwardText.Should().Be($"[b]Most Points In Loss[/b]{Environment.NewLine}Broadway St Hootinannies  for scoring 154.02 points in a loss.");
        }
    }
}