using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces
{
    public interface IScoreBoard
    {
        Guid StartGame(string homeTeam, string awayTeam);
        void FinishGame(Guid gameId);
        void UpdateGame(Guid gameId, int homeTeamScore, int awayTeamScore);
        void GetSummaryOfGames();
        List<Game> GetAllOngoingGames();
        List<Game> GetFinishedGames();
        Game GetOngoingGameById(Guid gameId);
    }
}
