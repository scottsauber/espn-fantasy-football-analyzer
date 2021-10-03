using System;

namespace EspnFantasyFootballAnalyzer.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal TruncateAfterTwoDecimalPlaces(this decimal value) => Math.Truncate(100 * value) / 100;
    }
}