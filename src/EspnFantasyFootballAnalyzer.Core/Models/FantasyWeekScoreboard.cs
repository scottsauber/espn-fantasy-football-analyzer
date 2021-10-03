using System.Collections.Generic;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyWeekScoreboard
    {
        public List<FantasyMatchup> FantasyMatchups { get; init; } = new();
    }
}