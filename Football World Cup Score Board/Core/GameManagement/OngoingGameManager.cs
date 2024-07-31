using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class OngoingGameManager : IOngoingGameManager
    {
        private readonly IGameRepository _gameRepository;

        public OngoingGameManager(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public List<Game> GetAllGames()
        {
            return _gameRepository
                .GetAllGames()
                .Where(game => !game.IsFinished)
                .ToList();
        }

        public Game GetGameById(Guid gameId)
        {
            return _gameRepository.GetGameById(gameId, false);
        }
    }
}
