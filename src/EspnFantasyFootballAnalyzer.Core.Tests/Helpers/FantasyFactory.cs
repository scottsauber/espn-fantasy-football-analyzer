﻿using AutoFixture;
using EspnFantasyFootballAnalyzer.Core.Models;

namespace EspnFantasyFootballAnalyzer.Core.Tests.Helpers;

public static class FantasyFactory
{
    public static FantasyMatchup CreateMatchupWithScores(int homeTeamScore, int awayTeamScore, FantasyTeam? homeTeam = null, FantasyPlayer? homeTeamPlayer = null)
    {
        return new FantasyMatchup
        {
            WeekNumber = 11,
            HomeTeam = CreateTeamResult(homeTeamScore, homeTeam, homeTeamPlayer),
            AwayTeam = CreateTeamResult(awayTeamScore),
        };
    }

    public static FantasyPlayer CreatePlayerWithPosition(FantasyPosition position)
    {
        var fixture = new Fixture();

        var player = fixture
            .Build<FantasyPlayer>()
            .With(x => x.Id, Random.Shared.Next(1, int.MaxValue))
            .Create();

        return player with
        {
            Position = position,
        };
    }

    private static FantasyTeamWeekResult CreateTeamResult(int score, FantasyTeam? fantasyTeam = null, FantasyPlayer? fantasyPlayer = null)
    {
        var fixture = new Fixture();
        return new FantasyTeamWeekResult
        {
            FantasyTeam = fantasyTeam ?? fixture.Create<FantasyTeam>(),
            StarterStats = new List<FantasyPlayerWeekStats>
            {
                new FantasyPlayerWeekStats
                {
                    FantasyPlayer = fantasyPlayer ?? fixture.Create<FantasyPlayer>(),
                    Score = score,
                }
            }
        };
    }
}