using EspnFantasyFootballAnalyzer.Core.Enums;
using EspnFantasyFootballAnalyzer.Core.Extensions;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.EspnData;

public interface IEspnDataMapperService
{
    FantasyWeekScoreboard Map(Root root, int weekNumber);
}

public class EspnDataMapperService : IEspnDataMapperService
{
    public static readonly string UndecidedWinner = "UNDECIDED";

    public FantasyWeekScoreboard Map(Root root, int weekNumber)
    {
        // Home and Away team are super close but not quite due to the structure coming back, but sharing as much code as I can relative to the amount of time I want to spend on this project
        var finalizedGames = root.Schedule
            .Where(x => x.Winner != UndecidedWinner && x.MatchupPeriodId == weekNumber)
            .ToList();
        return new FantasyWeekScoreboard(
            finalizedGames
                .Select(x => new FantasyMatchup
                {
                    WeekNumber = x.MatchupPeriodId,
                    HomeTeam = new FantasyTeamWeekResult
                    {
                        StarterStats = MapStarters(x.Home.RosterForMatchupPeriod),
                        BenchStats = MapBenchWarmers(x.Home.RosterForMatchupPeriod),
                        FantasyTeam = MapFantasyTeam(root.Teams.Single(t => t.Id == x.Home.TeamId)),
                    },
                    AwayTeam = new FantasyTeamWeekResult
                    {
                        StarterStats = MapStarters(x.Away.RosterForMatchupPeriod),
                        BenchStats = MapBenchWarmers(x.Away.RosterForMatchupPeriod),
                        FantasyTeam = MapFantasyTeam(root.Teams.Single(t => t.Id == x.Away.TeamId)),
                    }
                }).ToList());
    }

    private FantasyTeam MapFantasyTeam(Team team)
    {
        return new FantasyTeam
        {
            Id = team.Id,
            TeamName = team.Name,
        };
    }

    private List<FantasyPlayerWeekStats> MapStarters(RosterForMatchupPeriod rosterForMatchupPeriod)
    {
        return rosterForMatchupPeriod.Entries
            .Where(e => e.LineupSlotId != (int)LineupSlot.Bench)
            .Select(MapFantasyPlayerWeekStats)
            .ToList();
    }

    private List<FantasyPlayerWeekStats> MapBenchWarmers(RosterForMatchupPeriod rosterForMatchupPeriod)
    {
        return rosterForMatchupPeriod.Entries
            .Where(e => e.LineupSlotId == (int)LineupSlot.Bench)
            .Select(MapFantasyPlayerWeekStats)
            .ToList();
    }

    private static FantasyPlayerWeekStats MapFantasyPlayerWeekStats(Entry e)
    {
        return new()
        {
            Score = e.PlayerPoolEntry.AppliedStatTotal.TruncateAfterTwoDecimalPlaces(),
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
            Position = FantasyPosition.All.First(x => x.PositionId == e.PlayerPoolEntry.Player.DefaultPositionId)
        };
    }
}