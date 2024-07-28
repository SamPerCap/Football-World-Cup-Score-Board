namespace ScoreBoardLibrary.Models
{
    internal class Game
    {
        public Game(string homeTeamName, string awayTeamName)
        {
            HomeTeam = new Player { Name = homeTeamName, Score = 0 };
            AwayTeam = new Player { Name = awayTeamName, Score = 0 };
        }

        public Player HomeTeam { get; set; }
        public Player AwayTeam { get; set; }
        public int TotalScore => HomeTeam.Score + AwayTeam.Score;
    }

    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    internal class Audit
    {
        public DateTimeOffset Created { get; set; }
    }
}
