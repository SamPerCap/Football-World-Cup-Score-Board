using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class ScoreBoard : IScoreBoard
    {
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManager;
        private readonly IGameRepository _gameRepository;
        private readonly IGameManager _gameManager;

        public ScoreBoard(
            IOngoingGameManager ongoingGameManager,
            IFinishedGameManager finishedGameManager,
            IGameRepository gameRepository,
            IGameManager gameManager
            )
        {
            _ongoingGameManager = ongoingGameManager;
            _finishedGameManager = finishedGameManager;
            _gameRepository = gameRepository;
            _gameManager = gameManager;
        }

        public Guid StartGame(string homeTeam, string awayTeam)
        {
            return _gameManager.StartGame(homeTeam, awayTeam);
        }

        public void FinishGame(Guid gameId)
        {
            _gameManager.FinishGame(gameId);
        }

        public void UpdateGameScore(Guid gameId, int homeTeamScore, int awayTeamScore)
        {
            _gameManager.UpdateGame(gameId, homeTeamScore, awayTeamScore);
        }

        public List<Game> GetSummaryOfOngoingGames()
        {
            return _ongoingGameManager.GetAllGames();
        }

        public List<Game> GetSummaryOfFinishedGames()
        {
            return _finishedGameManager.GetAllGames();
        }

        public List<Game> GetSummaryOfAllHistoricGames()
        {
            return _gameManager.GetSummaryOfAllHistoricGames();
        }

        public List<Game> GetSummaryOfGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return _gameManager.GetSummaryOfGamesByDate(startDate, endDate);
        }
    }
}
