using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        void RemoveGame(Guid gameId);
        void UpdateGame(Game game);
        Game GetGameById(Guid gameId, bool? finished);
        List<Game> GetAllGames();
        List<Game> GetGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate);

    }
}
