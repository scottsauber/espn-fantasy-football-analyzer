using EspnFantasyFootballAnalyzer.Core.Awards;
using EspnFantasyFootballAnalyzer.Core.EspnData;

try
{
    Console.WriteLine("What year is this for?");
    int.TryParse(Console.ReadLine(), out var year);

    Console.WriteLine("What week is this for?");
    int.TryParse(Console.ReadLine(), out var week);

    var awardService = CreateAwardService();

    var winners = await awardService.GetAwardWinnersForWeekAsync(year, week);

    var output = string.Join(Environment.NewLine, winners.Select(x => x.AwardText));
    Console.WriteLine(output);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex}");
}

AwardService CreateAwardService()
{
    return new AwardService(new EspnDataMapperService(), new HttpClient());
}