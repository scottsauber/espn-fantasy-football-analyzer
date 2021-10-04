using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.RawData;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Awards
{
    public class AwardServiceTests
    {
        private readonly HttpClient _httpClient;

        public AwardServiceTests()
        {
            var testJson = File.ReadAllText(Path.Join("Awards", "test.json"));
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testJson)
                });
            _httpClient = new HttpClient(mockMessageHandler.Object);
        }
        
        [Fact]
        public async Task ShouldReturnCorrectMostPointsScoredTeam()
        {
            var awardService = new AwardService(new RawDataMapperService(), _httpClient);

            var awardWinners = await awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var mostPointsAward = awardWinners.Single(x => x.AwardId == AwardIds.MostPointsAward);
            mostPointsAward.AwardText.Should().Be("Most Points Scored by Waveland Woods Packers with 164.24 points.");
        }
        
        [Fact]
        public async Task ShouldReturnCorrectLeastPointsScoreTeam()
        {
            var awardService = new AwardService(new RawDataMapperService(), _httpClient);

            var awardWinners = await awardService.GetAwardWinnersForWeekAsync(2021, 3);

            var mostPointsAward = awardWinners.Single(x => x.AwardId == AwardIds.LeastPointsAward);
            mostPointsAward.AwardText.Should().Be("Least Points Scored by Team X-Bladz with 61.42 points.");
        }
    }
}