using System;

namespace EspnFantasyFootballAnalyzer.Core.Awards
{
    public static class AwardIds
    {
        public static Guid MostPointsAward => new("4A162503-E9F4-4EDD-B1F1-775688F4913C");
        public static Guid LeastPointsAward => new("5A5C9D6B-B16F-42C2-9F88-1DDFA5F3D9E4");
        public static Guid BiggestBlowoutAward => new("D0C16349-6213-4A06-8701-C784773D94B7");
        public static Guid SmallestMarginOfVictoryAward => new("6AA49C05-E16A-4B31-AC38-71B2E965F96D");
        public static Guid MostPointsByAQbStarterAward => new("D6EAEA81-095D-4C9A-8C92-7450CF87B5D6");
        public static Guid MostPointsByARbStarterAward => new("F8CD7543-49BC-4661-828C-A0FC4EDCA6DF");
    }
}