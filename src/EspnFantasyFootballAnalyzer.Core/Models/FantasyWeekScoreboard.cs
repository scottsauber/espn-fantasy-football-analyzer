using System.Collections.Generic;
using System.Linq;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyWeekScoreboard
    {
        public FantasyWeekScoreboard(List<FantasyMatchup> fantasyMatchups)
        {
            FantasyMatchups = fantasyMatchups;
        }
        
        public List<FantasyMatchup> FantasyMatchups { get; init; } = new();

        public int WeekNumber => FantasyMatchups.First().WeekNumber;

        public List<FantasyTeamWeekResult> AllTeams => FantasyMatchups
            .SelectMany(x => x.BothTeams)
            .ToList();
        
        public List<FantasyPlayerWeekStats> StarterStats => AllTeams
            .SelectMany(x => x.StarterStats)
            .ToList();

        public List<FantasyPlayerWeekStats> BenchStats => AllTeams
            .SelectMany(x => x.BenchStats)
            .ToList();
    }
}