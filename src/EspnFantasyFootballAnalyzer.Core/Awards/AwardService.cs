using System.Net.Http.Json;
using EspnFantasyFootballAnalyzer.Core.EspnData;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public interface IAwardService
    {
        Task<List<AwardWinner>> GetAwardWinnersForWeekAsync(int year, int weekNumber);
    }

    public class AwardService : IAwardService
    {
        private readonly IEspnDataMapperService _espnDataMapperService;
        private readonly HttpClient _httpClient;

        public AwardService(IEspnDataMapperService espnDataMapperService, HttpClient httpClient)
        {
            _espnDataMapperService = espnDataMapperService;
            _httpClient = httpClient;
        }

        public async Task<List<AwardWinner>> GetAwardWinnersForWeekAsync(int year, int weekNumber)
        {
            var awards = new List<IAward>
            {
                new MostPointsAward(),
                new LeastPointsAward(),
                new BiggestBlowoutAward(),
                new SmallestMarginOfVictoryAward(),
                new MostPointsInLossAward(),
                new MostPointsByAQbStarterAward(),
                new MostPointsByARbStarterAward(),
                new MostPointsByAWrStarterAward(),
                new MostPointsByATightEndStarterAward(),
                new MostPointsByADefenseSpecialTeamsStarterAward(),
            };

            var requestUri = $"https://fantasy.espn.com/apis/v3/games/ffl/seasons/{year}/segments/0/leagues/902814?scoringPeriodId={weekNumber}&view=modular&view=mScoreboard";
            var root = await _httpClient.GetFromJsonAsync<Root>(requestUri);

            var scoreboard = _espnDataMapperService.Map(root, weekNumber);

            return awards.Select(x => x.AssignAwardToWinner(scoreboard)).ToList();
        }
    }
}