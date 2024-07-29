using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces
{
    public interface IScoreBoard
    {
        void StartGame(string homeTeam, string awayTeam);
        void FinishGame();
        void UpdateGame();
        void GetSummaryOfGames();
        List<Game> GetOnGoingGames();
        List<Game> GetFinishedGames();
    }
}
