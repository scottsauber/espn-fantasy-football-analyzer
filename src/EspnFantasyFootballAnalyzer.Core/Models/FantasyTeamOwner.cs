namespace EspnFantasyFootballAnalyzer.Core.Models
{
    public record FantasyTeamOwner
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}