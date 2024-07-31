using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

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
            .OrderByDescending(game => game.TotalScore)
            .ThenByDescending(game => game.Audit.Created)
            .ToList();
    }

    public Game GetGameById(Guid gameId)
    {
        return _gameRepository.GetGameById(gameId, false);
    }
}
