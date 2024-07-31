using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class GameRepository : IGameRepository
    {
        private readonly List<Game> _games;

        public GameRepository()
        {
            _games = new();
        }

        public void AddGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game cannot be null.");
            }
            _games.Add(game);
        }

        public void UpdateGame(Game updatedGame)
        {
            Game game = GetGameById(updatedGame.Id, null);
            game = updatedGame;
        }

        public void RemoveGame(Guid gameId)
        {
            Game game = GetGameById(gameId, null);
            if (game != null)
            {
                _games.Remove(game);
            }
            else
            {
                throw new InvalidOperationException($"Game with Id {gameId} not found.");
            }
        }

        public Game GetGameById(Guid gameId, bool? finished)
        {
            try
            {
                if (finished.HasValue)
                {
                    return _games.Single(g => g.Id == gameId && g.IsFinished == finished);
                }

                return _games.Single(g => g.Id == gameId);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Game with Id {gameId} not found.");
            }
        }

        public List<Game> GetGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be earlier than end date.");
            }

            return GetAllGames()
                .Where(game => game.Audit.Created >= startDate && game.Audit.Created <= endDate)
                .ToList();
        }
        public List<Game> GetAllGames()
        {
            return _games
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }
    }
}
