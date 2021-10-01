using System.Collections.Generic;
using System.Linq;
using EspnFantasyFootballAnalyzer.Core.Enums;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.RawParser;

namespace EspnFantasyFootballAnalyzer.Core.RawData
{
    public class RawDataMapperService
    {
        public FantasyMatchup Map(Schedule schedule)
        {
            // Home and Away team are super close but not quite due to the structure coming back, but sharing as much code as I can relative to the amount of time I want to spend on this project
            return new FantasyMatchup
            {
                WeekNumber = schedule.MatchupPeriodId,
                HomeTeam = new FantasyTeamWeekResult
                {
                    StarterStats = MapStarters(schedule.Home.RosterForMatchupPeriod),
                    BenchStats = MapBenchWarmers(schedule.Home.RosterForMatchupPeriod),
                },
                AwayTeam = new FantasyTeamWeekResult
                {
                    StarterStats = MapStarters(schedule.Away.RosterForMatchupPeriod),
                    BenchStats = MapBenchWarmers(schedule.Away.RosterForMatchupPeriod),
                }
            };
        }

        private List<FantasyPlayerWeekStats> MapStarters(RosterForMatchupPeriod rosterForMatchupPeriod)
        {
            return rosterForMatchupPeriod.Entries
                .Where(e => e.LineupSlotId != (int) LineupSlot.Bench)
                .Select(MapFantasyPlayerWeekStats)
                .ToList();
        }
        
        private List<FantasyPlayerWeekStats> MapBenchWarmers(RosterForMatchupPeriod rosterForMatchupPeriod)
        {
            return rosterForMatchupPeriod.Entries
                .Where(e => e.LineupSlotId == (int) LineupSlot.Bench)
                .Select(MapFantasyPlayerWeekStats)
                .ToList();
        }

        private static FantasyPlayerWeekStats MapFantasyPlayerWeekStats(Entry e)
        {
            return new()
            {
                Score = e.PlayerPoolEntry.AppliedStatTotal,
                FantasyPlayer = MapFantasyPlayer(e),
            };
        }

        private static FantasyPlayer MapFantasyPlayer(Entry e)
        {
            return new()
            {
                Id = e.PlayerPoolEntry.Player.Id,
                FirstName = e.PlayerPoolEntry.Player.FirstName,
                LastName = e.PlayerPoolEntry.Player.LastName,
                Position = (FantasyPosition) e.PlayerPoolEntry.Player.DefaultPositionId
            };
        }
    }
}