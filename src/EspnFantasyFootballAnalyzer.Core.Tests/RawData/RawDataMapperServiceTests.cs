using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Enums;
using EspnFantasyFootballAnalyzer.Core.Models;
using EspnFantasyFootballAnalyzer.Core.RawData;
using EspnFantasyFootballAnalyzer.Core.RawParser;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

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
            var root = CreateRoot();
            var result = _rawDataMapperService.Map(root);

            result.WeekNumber.Should().Be(root.ScoringPeriodId);
        }

        [Fact]
        public void ShouldMapHomeAndAwayTeams()
        {
            var root = _fixture.Create<Root>();
            // Have to map Teams and Schedule Teams to marry up Id's
            root.Teams.Clear();
            root.Schedule = new List<Schedule>{root.Schedule.First()};
            var homeTeam = _fixture.Create<Team>();
            homeTeam.Location = "San Francisco";
            homeTeam.Nickname = "49ers";
            root.Teams.Add(homeTeam);
            root.Schedule[0].Home.TeamId = homeTeam.Id;
            var awayTeam = _fixture.Create<Team>();
            awayTeam.Location = "Chicago";
            awayTeam.Nickname = "Bears";
            root.Teams.Add(awayTeam);
            root.Schedule[0].Away.TeamId = awayTeam.Id;
        
            var result = _rawDataMapperService.Map(root);

            var fantasyMatchup = result.FantasyMatchups.Single();
            fantasyMatchup.HomeTeam.FantasyTeam.Id.Should().Be(homeTeam.Id);
            fantasyMatchup.HomeTeam.FantasyTeam.TeamName.Should().Be("San Francisco 49ers");
            fantasyMatchup.AwayTeam.FantasyTeam.Id.Should().Be(awayTeam.Id);
            fantasyMatchup.AwayTeam.FantasyTeam.TeamName.Should().Be("Chicago Bears");
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
            var root = CreateRoot(lineupSlot);
            var entries = new List<Entry>();
            var player = _fixture.Create<Player>();
            entries.Add(CreateEntriesForTeam(expectedStarterScore, lineupSlot, player));
            entries.Add(CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player));
            root.Schedule.Single().Home.RosterForMatchupPeriod.Entries = entries;
        
            var result = _rawDataMapperService.Map(root);
        
            result.FantasyMatchups.Single().HomeTeam.TotalStarterScore.Should().Be(expectedStarterScore);
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
            var root = CreateRoot();
            var entries = new List<Entry>();
            var player = _fixture.Create<Player>();
            entries.Add(CreateEntriesForTeam(expectedStarterScore, lineupSlot, player));
            entries.Add(CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player));
            root.Schedule.Single().Away.RosterForMatchupPeriod.Entries = entries;
        
            var result = _rawDataMapperService.Map(root);
        
            result.FantasyMatchups.Single().AwayTeam.TotalStarterScore.Should().Be(expectedStarterScore);
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
            var root = CreateRoot();
            var expectedStarterScore = _fixture.Create<int>();
            var player = _fixture.Create<Player>();
            player.DefaultPositionId = (int) FantasyPosition.Quarterback;
            var entries = new List<Entry>
            {
                CreateEntriesForTeam(expectedStarterScore, lineupSlot, player),
            };
            root.Schedule.Single().Home.RosterForMatchupPeriod.Entries = entries;
        
            var result = _rawDataMapperService.Map(root);
        
            var starterStats = result.FantasyMatchups.Single().HomeTeam.StarterStats.Single();
            starterStats.Score.Should().Be(expectedStarterScore);
            starterStats.FantasyPlayer.Id.Should().Be(player.Id);
            starterStats.FantasyPlayer.FirstName.Should().Be(player.FirstName);
            starterStats.FantasyPlayer.LastName.Should().Be(player.LastName);
            starterStats.FantasyPlayer.Position.Should().Be(FantasyPosition.Quarterback);
        }
        
        [Fact]
        public void ShouldMapBenchDataCorrectly()
        {
            var root = CreateRoot();
            var expectedStarterScore = _fixture.Create<int>();
            var player = _fixture.Create<Player>();
            player.DefaultPositionId = (int) FantasyPosition.Quarterback;
            var entries = new List<Entry>
            {
                CreateEntriesForTeam(expectedStarterScore, LineupSlot.Bench, player),
            };
            root.Schedule.Single().Home.RosterForMatchupPeriod.Entries = entries;
        
            var result = _rawDataMapperService.Map(root);
        
            var benchStats = result.FantasyMatchups.Single().HomeTeam.BenchStats.Single();
            benchStats.Score.Should().Be(expectedStarterScore);
            benchStats.FantasyPlayer.Id.Should().Be(player.Id);
            benchStats.FantasyPlayer.FirstName.Should().Be(player.FirstName);
            benchStats.FantasyPlayer.LastName.Should().Be(player.LastName);
            benchStats.FantasyPlayer.Position.Should().Be(FantasyPosition.Quarterback);
        }

        
        private Root CreateRoot(LineupSlot lineupSlot = LineupSlot.Bench)
        {
            var root = _fixture.Create<Root>();
            root.Teams.Clear();
            root.Schedule = new List<Schedule>{CreateSchedule(lineupSlot)};
            var homeTeam = _fixture.Create<Team>();
            root.Teams.Add(homeTeam);
            root.Schedule[0].Home.TeamId = homeTeam.Id;
            var awayTeam = _fixture.Create<Team>();
            root.Teams.Add(awayTeam);
            root.Schedule[0].Away.TeamId = awayTeam.Id;
            return root;
        }
        
        private static Schedule CreateSchedule(LineupSlot lineupSlot)
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