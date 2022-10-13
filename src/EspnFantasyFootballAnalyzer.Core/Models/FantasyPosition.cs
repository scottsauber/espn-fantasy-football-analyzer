namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyPosition
    {
        public int PositionId { get; }
        public string PositionName { get; }

        private FantasyPosition(int positionId, string positionName)
        {
            PositionId = positionId;
            PositionName = positionName;
        }
        
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