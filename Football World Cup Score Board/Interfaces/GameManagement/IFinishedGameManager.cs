using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces.GameManagement
{
    public interface IFinishedGameManager
    {
        List<Game> GetAllGames();
        Game GetGameById(Guid gameId);
    }
}
