using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces.GameManagement
{
    public interface IOngoingGameManager
    {
        List<Game> GetAllGames();
        Game GetGameById(Guid gameId);
    }
}
