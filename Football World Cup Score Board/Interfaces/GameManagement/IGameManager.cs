using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces.GameManagement
{
    public interface IGameManager
    {
        Guid StartGame(string homeTeam, string awayTeam);
        void FinishGame(Guid gameId);
        void UpdateGame(Guid gameId, int homeTeamScore, int awayTeamScore);

        List<Game> GetSummaryOfAllHistoricGames();
        List<Game> GetSummaryOfGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate);
    }

}
