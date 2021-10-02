using System;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public class AwardWinner
    {
        public FantasyTeam FantasyTeam { get; set; }
        public Guid AwardId { get; set; }
        public int WeekNumber { get; set; }
    }
}