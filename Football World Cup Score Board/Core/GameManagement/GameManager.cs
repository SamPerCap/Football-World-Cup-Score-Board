using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class GameManager : IGameManager
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamManager _teamManager;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManager;

        public GameManager(IGameRepository gameRepository, ITeamManager teamManager, IOngoingGameManager ongoingGameManager, IFinishedGameManager finishedGameManager)
        {
            _gameRepository = gameRepository;
            _teamManager = teamManager;
            _ongoingGameManager = ongoingGameManager;
            _finishedGameManager = finishedGameManager;
        }

        public Guid StartGame(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                throw new ArgumentException($"Team names have not been provided: [{homeTeam}] vs [{awayTeam}]");
            }

            if (_teamManager.IsTeamPlaying(homeTeam) || _teamManager.IsTeamPlaying(awayTeam))
            {
                var existingTeam = _teamManager.IsTeamPlaying(homeTeam) ? homeTeam : awayTeam;
                throw new InvalidOperationException($"The team ({existingTeam}) is already playing a game.");
            }

            Game game = new(homeTeam, awayTeam);

            _gameRepository.AddGame(game);

            _teamManager.AddTeam(homeTeam);
            _teamManager.AddTeam(awayTeam);

            return game.Id;
        }

        public void FinishGame(Guid gameId)
        {
            Game game = _ongoingGameManager.GetGameById(gameId);
            game.IsFinished = true;
            
            _gameRepository.UpdateGame(game);

            _teamManager.RemoveTeam(game.HomeTeam.Name);
            _teamManager.RemoveTeam(game.AwayTeam.Name);
        }

        public void UpdateGame(Guid gameId, int homeTeamScore, int awayTeamScore)
        {
            Game game = _ongoingGameManager.GetGameById(gameId);
            
            game.HomeTeam.Score = homeTeamScore;
            game.AwayTeam.Score = awayTeamScore;

            _gameRepository.UpdateGame(game);
        }

        public List<Game> GetSummaryOfAllHistoricGames()
        {
            return _gameRepository.GetAllGames();
        }

        public List<Game> GetSummaryOfGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }

            return _ongoingGameManager.GetAllGames()
                .Concat(_finishedGameManager.GetAllGames())
                .Where(game => game.Audit.Created >= startDate && game.Audit.Created <= endDate)
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }
    }
}
