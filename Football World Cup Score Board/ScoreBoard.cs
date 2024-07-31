using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class ScoreBoard : IScoreBoard
    {
        private readonly List<Game> _ongoingGames;
        private readonly List<Game> _finishedGames;
        private readonly HashSet<string> _teamsPlaying;

        public ScoreBoard()
        {
            _ongoingGames = [];
            _finishedGames = [];
            _teamsPlaying = [];
        }

        public Guid StartGame(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                throw new ArgumentException($"Team names have not been provided: [{homeTeam}] vs [{awayTeam}]");
            }

            if (_teamsPlaying.Contains(homeTeam) || _teamsPlaying.Contains(awayTeam))
            {
                var existingTeam = _teamsPlaying.Contains(homeTeam) ? homeTeam : awayTeam;
                throw new InvalidOperationException($"The team ({existingTeam}) is already playing a game.");
            }

            Game game = new(homeTeam, awayTeam);
            _ongoingGames.Add(game);
            _teamsPlaying.Add(homeTeam);
            _teamsPlaying.Add(awayTeam);

            return game.Id;
        }

        public void FinishGame(Guid gameId)
        {
            Game game = GetOngoingGameById(gameId);
            game.IsFinished = true;
            _ongoingGames.Remove(game);
            _finishedGames.Add(game);
        }

        public void UpdateGame(Guid gameId, int homeTeamScore, int awayTeamScore)
        {
            Game game = GetOngoingGameById(gameId);
            game.HomeTeam.Score = homeTeamScore;
            game.AwayTeam.Score = awayTeamScore;
        }

        public Game GetOngoingGameById(Guid gameId)
        {
            try
            {
                return _ongoingGames.Single(game => game.Id == gameId);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Error obtaining game with Id: {gameId}.", ex);
            }
        }

        public Game GetFinishedGameById(Guid gameId)
        {
            try
            {
                return _finishedGames.Single(game => game.Id == gameId);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Error obtaining game with Id: {gameId}.", ex);
            }
        }

        public List<Game> GetSummaryOfOngoingGames()
        {
            return _ongoingGames
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }

        public List<Game> GetSummaryOfFinishedGames()
        {
            return _finishedGames
               .OrderByDescending(game => game.TotalScore)
               .ThenByDescending(game => game.Audit.Created)
               .ToList();
        }

        public List<Game> GetSummaryOfAllHistoricGames()
        {
            return _ongoingGames
                .Concat(_finishedGames)
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }

        public List<Game> GetSummaryOfGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }

            return _ongoingGames
                .Concat(_finishedGames)
                .Where(game => game.Audit.Created >= startDate && game.Audit.Created <= endDate)
                .OrderByDescending(game => game.TotalScore)
                .ThenByDescending(game => game.Audit.Created)
                .ToList();
        }
    }
}
