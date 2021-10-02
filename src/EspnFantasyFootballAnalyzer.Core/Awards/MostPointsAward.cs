using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsAward : IAward
    {
        public string AwardName => "Most Points";
        public string AwardId { get; }

        public AwardWinner AssignAwardToWinner(FantasyWeekScoreboard weekScoreboard)
        {
            return new AwardWinner
            {
                WeekNumber = weekScoreboard.WeekNumber,
                FantasyTeam = weekScoreboard.FantasyMatchups
                    .Select(x => x.Winner)
                    .OrderByDescending(x => x.TotalStarterScore)
                    .First()
                    .FantasyTeam,
            };
        }
    }
}