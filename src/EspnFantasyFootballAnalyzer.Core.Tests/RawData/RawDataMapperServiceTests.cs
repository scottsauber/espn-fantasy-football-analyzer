using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Enums;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.RawData;
using EspnFantasyFootballAnalyzer.Core.RawParser;
using FluentAssertions;
using Xunit;

namespace EspnFantasyFootballAnalyzer.Core.Tests.RawData
{
    public class RawDataMapperServiceTests
    {
        private readonly RawDataMapperService _rawDataMapperService;
        private readonly Fixture _fixture;

        public RawDataMapperServiceTests()
        {
            _rawDataMapperService = new RawDataMapperService();
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldMapMatchupPeriodToWeekNumber()
        {
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 17));
            var expectedWeekNumber = _fixture.Create<int>();
            var schedule = CreateSchedule();
            schedule.MatchupPeriodId = expectedWeekNumber;

            var result = _rawDataMapperService.Map(schedule);

            result.WeekNumber.Should().Be(expectedWeekNumber);
        }

        [Theory]
        [InlineData(LineupSlot.StartingFlex)]
        [InlineData(LineupSlot.StartingQuarterback)]
        [InlineData(LineupSlot.StartingRunningBack)]
        [InlineData(LineupSlot.StartingTightEnd)]
        [InlineData(LineupSlot.StartingWideReceiver)]
        [InlineData(LineupSlot.StartingDefenseSpecialTeams)]
        public void ShouldMapTotalStarterScoreForHomeTeamBasedOnStarterScoreNotBench(LineupSlot lineupSlot)
        {
            var expectedStarterScore = _fixture.Create<decimal>();
            var schedule = CreateSchedule();
            var entries = new List<Entry>();
            var player = _fixture.Create<Player>();
            entries.Add(CreateEntriesForTeam(expectedStarterScore, lineupSlot, player));
            entries.Add(CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player));
            schedule.Home.RosterForMatchupPeriod.Entries = entries;

            var result = _rawDataMapperService.Map(schedule);

            result.HomeTeam.TotalStarterScore.Should().Be(expectedStarterScore);
        }

        [Theory]
        [InlineData(LineupSlot.StartingFlex)]
        [InlineData(LineupSlot.StartingQuarterback)]
        [InlineData(LineupSlot.StartingRunningBack)]
        [InlineData(LineupSlot.StartingTightEnd)]
        [InlineData(LineupSlot.StartingWideReceiver)]
        [InlineData(LineupSlot.StartingDefenseSpecialTeams)]
        public void ShouldMapTotalStarterScoreForAwayTeamBasedOnStarterScoreNotBench(LineupSlot lineupSlot)
        {
            var expectedStarterScore = _fixture.Create<decimal>();
            var schedule = CreateSchedule();
            var entries = new List<Entry>();
            var player = _fixture.Create<Player>();
            entries.Add(CreateEntriesForTeam(expectedStarterScore, lineupSlot, player));
            entries.Add(CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player));
            schedule.Away.RosterForMatchupPeriod.Entries = entries;

            var result = _rawDataMapperService.Map(schedule);

            result.AwayTeam.TotalStarterScore.Should().Be(expectedStarterScore);
        }

        [Theory]
        [InlineData(LineupSlot.StartingFlex)]
        [InlineData(LineupSlot.StartingQuarterback)]
        [InlineData(LineupSlot.StartingRunningBack)]
        [InlineData(LineupSlot.StartingTightEnd)]
        [InlineData(LineupSlot.StartingWideReceiver)]
        [InlineData(LineupSlot.StartingDefenseSpecialTeams)]
        public void ShouldMapStarterDataCorrectly(LineupSlot lineupSlot)
        {
            var schedule = CreateSchedule();
            var expectedStarterScore = _fixture.Create<int>();
            var player = _fixture.Create<Player>();
            player.DefaultPositionId = (int) FantasyPosition.Quarterback;
            var entries = new List<Entry>
            {
                CreateEntriesForTeam(expectedStarterScore, lineupSlot, player),
            };
            schedule.Home.RosterForMatchupPeriod.Entries = entries;

            var result = _rawDataMapperService.Map(schedule);

            var starterStats = result.HomeTeam.StarterStats.Single();
            starterStats.Score.Should().Be(expectedStarterScore);
            starterStats.FantasyPlayer.Id.Should().Be(player.Id);
            starterStats.FantasyPlayer.FirstName.Should().Be(player.FirstName);
            starterStats.FantasyPlayer.LastName.Should().Be(player.LastName);
            starterStats.FantasyPlayer.Position.Should().Be(FantasyPosition.Quarterback);
        }

        [Fact]
        public void ShouldMapBenchDataCorrectly()
        {
            var schedule = CreateSchedule();
            var expectedStarterScore = _fixture.Create<int>();
            var player = _fixture.Create<Player>();
            player.DefaultPositionId = (int) FantasyPosition.Quarterback;
            var entries = new List<Entry>
            {
                CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player),
            };
            schedule.Home.RosterForMatchupPeriod.Entries = entries;

            var result = _rawDataMapperService.Map(schedule);

            var benchStats = result.HomeTeam.BenchStats.Single();
            benchStats.Score.Should().Be(expectedStarterScore);
            benchStats.FantasyPlayer.Id.Should().Be(player.Id);
            benchStats.FantasyPlayer.FirstName.Should().Be(player.FirstName);
            benchStats.FantasyPlayer.LastName.Should().Be(player.LastName);
            benchStats.FantasyPlayer.Position.Should().Be(FantasyPosition.Quarterback);
        }

        private static Schedule CreateSchedule(LineupSlot lineupSlot = LineupSlot.Bench)
        {
            return new Schedule
            {
                MatchupPeriodId = 2,
                Home = new Home
                {
                    RosterForMatchupPeriod = CreateRosterForMatchupPeriod(lineupSlot)
                },
                Away = new Away
                {
                    RosterForMatchupPeriod = CreateRosterForMatchupPeriod(lineupSlot)
                }
            };
        }

        private static RosterForMatchupPeriod CreateRosterForMatchupPeriod(LineupSlot lineupSlot)
        {
            return new RosterForMatchupPeriod
            {
                Entries = new List<Entry>
                {
                    new()
                    {
                        PlayerId = 10_000,
                        PlayerPoolEntry = new PlayerPoolEntry
                        {
                            AppliedStatTotal = 100,
                            Player = CreatePlayer("Spongebob", "SquarePpants", FantasyPosition.Quarterback),
                        },
                        LineupSlotId = (int) lineupSlot,
                    },
                    new()
                    {
                        PlayerId = 1_000,
                        PlayerPoolEntry = new PlayerPoolEntry
                        {
                            AppliedStatTotal = 50,
                            Player = CreatePlayer("Patrick", "Star", FantasyPosition.RunningBack),
                        },
                        LineupSlotId = (int) LineupSlot.Bench,
                    }
                }
            };
        }

        private static Player CreatePlayer(string firstName, string lastName, FantasyPosition fantasyPosition)
        {
            return new Player
            {
                FirstName = firstName,
                LastName = lastName,
                DefaultPositionId = (int) fantasyPosition,
            };
        }

        private Entry CreateEntriesForTeam(decimal expectedStarterScore, LineupSlot lineupSlot, Player player)
        {
            return new()
            {
                PlayerPoolEntry = new PlayerPoolEntry
                {
                    AppliedStatTotal = expectedStarterScore,
                    Player = player,
                },
                LineupSlotId = (int) lineupSlot,
            };
        }
    }
}