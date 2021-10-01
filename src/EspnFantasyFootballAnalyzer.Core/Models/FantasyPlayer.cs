namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyPlayer
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public FantasyPosition Position { get; init; }
    }
}