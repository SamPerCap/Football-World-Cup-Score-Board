using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces
{
    public interface IScoreBoard
    {
        Guid StartGame(string homeTeam, string awayTeam);
        void FinishGame(Guid gameId);
        void UpdateGame();
        void GetSummaryOfGames();
        List<Game> GetOnGoingGames();
        List<Game> GetFinishedGames();
    }
}
