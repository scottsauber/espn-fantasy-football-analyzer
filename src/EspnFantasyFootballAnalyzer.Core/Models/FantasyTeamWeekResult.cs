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
        public List<FantasyPlayerWeekStats> StarterStats { get; init; } = new();
        public List<FantasyPlayerWeekStats> BenchStats { get; init; } = new();
        public FantasyTeam FantasyTeam { get; set; }
    }
}