using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByAWrStarterAward : MostPointsByAPositionAward
    {
        public override Guid AwardId => AwardIds.MostPointsByAWrStarterAward;
        public override FantasyPosition FantasyPosition => FantasyPosition.WideReceiver;
    }
}