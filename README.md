# espn-fantasy-football-analyzer

Purpose is to create a weekly "awards" of an ESPN league I have with some friends.

Also using this to experiment with different libraries and approaches. Namely... I hate random test data, but everyone seems to do it so I'm going to use it on this to either help me confirm my hate or not. So far it's only confirmed my hate. Likely will end up ripping it all out so I can see a diff.

Some ideas of weekly awards to generate (roughly in order of easiest to hardest):

- Highest Scorer ✔️
- Lowest Scorer ✔️
- Biggest Blowout ✔️
- Smallest victory ✔️
- SNF/MNF Victory
- Best Manager (Highest % points starting vs bench)
- Worst Manager (Highest % points on bench vs starting)
- Overachiever (highest over projection)
- Underachiever (most below projection)
- Starting Positions of the week (QB✔️/RB✔️/WR✔️/TE✔️/DEF✔️)
- Bench Positions of the week (QB/RB/WR/TE/DEF)
- Worst decision of the week

Other ideas:

- Lucky (how much below normal did opponent score)
- Unlucky (how much above normal did opponent score)
- Opponent Ranking Low/High
- Misfits (team with most undrafteds or didn't drafts)
- Most Injuries (team with most injured players on roster)
- Most Bye Weeks (team with most upcoming bye weeks)
- Before Week Analysis of Byes/Opponents/Expectations
- Best Rookies
- Best people >30

Season ending awards:

- Biggest pre-season surprise
- Biggest pre-season letdown

API Examples:

- Individual matchups - https://fantasy.espn.com/apis/v3/games/ffl/seasons/2021/segments/0/leagues/902814?view=mBoxscore&view=mMatchupScore&view=mRoster&view=mSettings&view=mStatus&view=mTeam&view=modular&view=mNav

Need:

- Parse schedule
- Parse individual box scores
- Compare against box scores of ^

Notes:

- "Slots" to determine starter, can use React Components extension to see which slot is which

## Usage

1. Install .NET 6
2. `cd src/EspnFantasyFootballAnalyzer.Console`
3. `dotnet run`
