using System;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByARbStarterAward : MostPointsByAPositionAward
    {
        public override Guid AwardId => AwardIds.MostPointsByARbStarterAward;
        public override FantasyPosition FantasyPosition => FantasyPosition.RunningBack;
    }
}