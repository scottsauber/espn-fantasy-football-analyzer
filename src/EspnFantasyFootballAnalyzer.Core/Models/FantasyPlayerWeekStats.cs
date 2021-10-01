namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyPlayerWeekStats
    {
        public FantasyPlayer FantasyPlayer { get; set; }
        public decimal Score { get; init; }
    }
}