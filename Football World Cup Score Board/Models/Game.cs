namespace ScoreBoardLibrary.Models
{
    public class Game
    {
        public Game(string homeTeamName, string awayTeamName)
        {
            Id = Guid.NewGuid();
            HomeTeam = new Player { Name = homeTeamName, Score = 0 };
            AwayTeam = new Player { Name = awayTeamName, Score = 0 };
        }

        public Guid Id { get; set; }
        public Player HomeTeam { get; set; }
        public Player AwayTeam { get; set; }
        public int TotalScore => HomeTeam.Score + AwayTeam.Score;
        public bool IsFinished { get; set; } = false;
        
        public Audit Audit { get; set; }
    }
}
