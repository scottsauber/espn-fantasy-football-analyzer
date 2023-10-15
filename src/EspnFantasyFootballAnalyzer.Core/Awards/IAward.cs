using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards;

public interface IAward
{
    AwardWinner AssignAwardToWinner(FantasyWeekScoreboard fantasyWeekScoreboard);
}