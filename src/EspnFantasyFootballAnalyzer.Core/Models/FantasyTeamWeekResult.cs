using System.Collections.Generic;
using System.Linq;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyTeamWeekResult
    {
        public int FantasyTeamId { get; init; }
        public decimal TotalStarterScore => StarterStats.Sum(s => s.Score);
        public decimal TotalBenchScore => BenchStats.Sum(s => s.Score);
        public List<FantasyPlayerWeekStats> StarterStats { get; init; } = new();
        public List<FantasyPlayerWeekStats> BenchStats { get; init; } = new();
    }
}