using System;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByATightEndStarterAward : MostPointsByAPositionAward
    {
        public override Guid AwardId => AwardIds.MostPointsByATightEndStarterAward;
        public override FantasyPosition FantasyPosition => FantasyPosition.TightEnd;
    }
}