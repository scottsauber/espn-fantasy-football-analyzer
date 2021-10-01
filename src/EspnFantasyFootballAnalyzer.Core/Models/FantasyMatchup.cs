﻿namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyMatchup
    {
        public int WeekNumber { get; init; }
        public FantasyTeamWeekResult HomeTeam { get; init; }
        public FantasyTeamWeekResult AwayTeam { get; init; }
    }
}