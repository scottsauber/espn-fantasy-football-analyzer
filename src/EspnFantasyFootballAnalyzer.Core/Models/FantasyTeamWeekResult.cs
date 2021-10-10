using System;
using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Extensions;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyTeamWeekResult
    {
        public decimal TotalStarterScore => StarterStats.Sum(s => s.Score).TruncateAfterTwoDecimalPlaces();
        public decimal TotalBenchScore => BenchStats.Sum(s => s.Score).TruncateAfterTwoDecimalPlaces();
        public List<FantasyPlayerWeekStats> StarterStats { get; set; } = new();
        public List<FantasyPlayerWeekStats> BenchStats { get; set; } = new();
        public FantasyTeam FantasyTeam { get; set; }
    }
}