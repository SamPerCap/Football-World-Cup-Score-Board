using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary
{
    public class ScoreBoard : IScoreBoard
    {
        private readonly List<Game> _onGoingGames;
        private readonly List<Game> _finishedGames;
        private readonly HashSet<string> _teamsPlaying;

        public ScoreBoard()
        {
            _onGoingGames = [];
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

            var game = new Game(homeTeam, awayTeam);
            _onGoingGames.Add(game);
            _teamsPlaying.Add(homeTeam);
            _teamsPlaying.Add(awayTeam);

            return game.Id;
        }

        public void FinishGame(Guid gameId)
        {
            try
            {
                var finishedGame = _onGoingGames.Single(game => game.Id == gameId);
                finishedGame.IsFinished = true;
                _onGoingGames.Remove(finishedGame);
                _finishedGames.Add(finishedGame);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Game with Id {gameId} has not been found");
            }
        }

        public void GetSummaryOfGames()
        {
            throw new NotImplementedException();
        }

        public void UpdateGame()
        {
            throw new NotImplementedException();
        }

        public List<Game> GetOnGoingGames()
        {
            return _onGoingGames;
        }

        public List<Game> GetFinishedGames()
        {
            return _finishedGames;
        }
    }
}
