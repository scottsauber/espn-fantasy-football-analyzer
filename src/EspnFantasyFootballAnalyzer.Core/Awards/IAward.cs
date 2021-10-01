using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public interface IAward
    {
        string AwardName { get; }
        string AwardId { get; }
        AwardWinner AssignAwardToWinner(FantasyWeekScoreboard weekScoreboard);
    }
}