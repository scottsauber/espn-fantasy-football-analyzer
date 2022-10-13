using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByAQbStarterAward : MostPointsByAPositionAward
    {
        public override Guid AwardId => AwardIds.MostPointsByAQbStarterAward;
        public override FantasyPosition FantasyPosition => FantasyPosition.Quarterback;
    }
}