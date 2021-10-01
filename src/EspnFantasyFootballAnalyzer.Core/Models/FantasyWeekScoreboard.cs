using System.Collections.Generic;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyWeekScoreboard
    {
        public int WeekNumber { get; init; }
        public List<FantasyMatchup> FantasyMatchups { get; init; } = new();
    }
}