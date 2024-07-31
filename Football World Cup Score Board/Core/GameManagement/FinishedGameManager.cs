using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class FinishedGameManager : IFinishedGameManager
    {
        private readonly IGameRepository _gameRepository;

        public FinishedGameManager(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public List<Game> GetAllGames()
        {
            return _gameRepository.GetAllGames()
                .Where(game => game.IsFinished)
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }

        public Game GetGameById(Guid gameId)
        {
            return _gameRepository.GetGameById(gameId, true);
        }
    }
}
