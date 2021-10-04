using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EspnFantasyFootballAnalyzer.Core.RawData;
using EspnFantasyFootballAnalyzer.Core.RawParser;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public interface IAwardService
    {
        Task<List<AwardWinner>> GetAwardWinnersForWeekAsync(int year, int weekNumber);
    }

    public class AwardService : IAwardService
    {
        private readonly IRawDataMapperService _rawDataMapperService;
        private readonly HttpClient _httpClient;

        public AwardService(IRawDataMapperService rawDataMapperService, HttpClient httpClient)
        {
            _rawDataMapperService = rawDataMapperService;
            _httpClient = httpClient;
        }
        
        public async Task<List<AwardWinner>> GetAwardWinnersForWeekAsync(int year, int weekNumber)
        {
            var awards = new List<IAward>
            {
                new MostPointsAward(),
                new LeastPointsAward(),
                new BiggestBlowoutAward(),
            };

            var requestUri = $"https://fantasy.espn.com/apis/v3/games/ffl/seasons/{year}/segments/0/leagues/902814?scoringPeriodId={weekNumber}&view=modular&view=mScoreboard";
            var root = await _httpClient.GetFromJsonAsync<Root>(requestUri);

            var fantasyMatchups = _rawDataMapperService.Map(root, weekNumber)
                .FantasyMatchups
                .Where(x => x.WeekNumber == weekNumber)
                .ToList();
            
            return awards.Select(x => x.AssignAwardToWinner(fantasyMatchups, weekNumber)).ToList();
        }
    }
}