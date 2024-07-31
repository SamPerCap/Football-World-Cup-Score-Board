using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;
using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Tests
{
    public class StartGameTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public StartGameTests()
        {
            _teamManager = new TeamManager();
            _gameRepository = new GameRepository();
            _ongoingGameManager = new OngoingGameManager(_gameRepository);
            _finishedGameManger = new FinishedGameManager(_gameRepository);
            _gameManager = new GameManager(_gameRepository, _teamManager, _ongoingGameManager, _finishedGameManger);

            _scoreBoard = new ScoreBoard(
                _ongoingGameManager,
                _finishedGameManger,
                _gameManager
            );
        }

        [Theory]
        [InlineData(null, "Away Team")]
        [InlineData("Home Team", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ShouldThrowExceptionIfNoTeams(string homeTeam, string awayTeam)
        {
            Assert.Throws<ArgumentException>(() => _scoreBoard.StartGame(homeTeam, awayTeam));
        }

        [Theory]
        [InlineData("Home Team", "Away Team")]
        public void ShouldInitializeGameWithValidTeamsAndScore(string homeTeam, string awayTeam)
        {
            // Act
            _scoreBoard.StartGame(homeTeam, awayTeam);
            List<Game> ongoingGames = _scoreBoard.GetSummaryOfOngoingGames();
            Game game = ongoingGames[0];

            // Assert
            Assert.Equal(homeTeam, game.HomeTeam.Name);
            Assert.Equal(awayTeam, game.AwayTeam.Name);
            Assert.Equal(0, game.HomeTeam.Score);
            Assert.Equal(0, game.AwayTeam.Score);
            Assert.Equal(0, game.TotalScore);
        }

        [Theory]
        [InlineData("Home Team", "Away Team")]
        public void ShouldPreventDuplicated(string homeTeam, string awayTeam)
        {
            // Act
            _scoreBoard.StartGame(homeTeam, awayTeam);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _scoreBoard.StartGame(homeTeam, awayTeam));
            Assert.Single(_scoreBoard.GetSummaryOfOngoingGames());
        }
    }
}