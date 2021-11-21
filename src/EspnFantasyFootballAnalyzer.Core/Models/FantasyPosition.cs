using System.Collections.Generic;

namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyPosition(int PositionId, string PositionName)
    {
        public static FantasyPosition Quarterback => new(1, "Quarterback");
        public static FantasyPosition RunningBack => new(2, "Running Back");
        public static FantasyPosition WideReceiver => new(3, "Wide Receiver");
        public static FantasyPosition TightEnd => new(4, "Tight End");
        public static FantasyPosition DefenseSpecialTeams => new(16, "Defense/Special Teams");

        public static List<FantasyPosition> All => new()
        {
            Quarterback,
            RunningBack,
            WideReceiver,
            TightEnd,
            DefenseSpecialTeams
        };
    }
}