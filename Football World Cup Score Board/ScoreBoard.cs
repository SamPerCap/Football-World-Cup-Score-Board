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

        public void StartGame(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                throw new ArgumentException("Team names have not been provided");
            }

            if (_teamsPlaying.Contains(homeTeam) || _teamsPlaying.Contains(awayTeam))
            {
                throw new InvalidOperationException("One team is already playing a game");
            }

            var game = new Game(homeTeam, awayTeam);
            _onGoingGames.Add(game);
            _teamsPlaying.Add(homeTeam);
            _teamsPlaying.Add(awayTeam);
        }

        public void FinishGame()
        {
            throw new NotImplementedException();
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
