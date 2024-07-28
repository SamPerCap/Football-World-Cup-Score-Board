namespace ScoreBoardLibrary.Interfaces
{
    public interface IScoreBoard
    {
        void StartGame(string homeTeam, string awayTeam);
        void FinishGame();
        void UpdateGame();
        void GetSummaryOfGames();
    }
}
