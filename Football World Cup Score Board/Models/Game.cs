namespace Football_World_Cup_Score_Board.Models
{
    internal class Game
    {
        public Player HomeTeam { get; set; }
        public Player AwayTeam { get; set; }
        public int TotalScore { get; set; }
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
