using System.Collections.Generic;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public interface IAward
    {
        AwardWinner AssignAwardToWinner(List<FantasyMatchup> fantasyMatchups, int weekNumber);
    }
}