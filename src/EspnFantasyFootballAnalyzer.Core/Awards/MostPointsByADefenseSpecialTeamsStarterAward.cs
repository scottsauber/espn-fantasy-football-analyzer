using System;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class MostPointsByADefenseSpecialTeamsStarterAward : MostPointsByAPositionAward
    {
        public override Guid AwardId => AwardIds.MostPointsByADefenseSpecialTeamsAward;
        public override FantasyPosition FantasyPosition => FantasyPosition.DefenseSpecialTeams;
    }
}