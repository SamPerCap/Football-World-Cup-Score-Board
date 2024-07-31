using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;

namespace ScoreBoardLibrary.Tests
{
    public class UpdateGameTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public UpdateGameTests()
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

        [Fact]
        public void ShouldThrowExceptionIfMatchNotFound()
        {
            // Arrange
            string homeTeam = "Home Team";
            string awayTeam = "Away Team";

            // Act
            _scoreBoard.StartGame(homeTeam, awayTeam);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _scoreBoard.UpdateGameScore(Guid.NewGuid(), 1, 0));
        }

        [Fact]
        public void ShouldUpdateTheCorrectGameOnce()
        {
            // Arrange
            string homeTeam = "Home Team";
            string awayTeam = "Away Team";

            // Act
            var id = _scoreBoard.StartGame(homeTeam, awayTeam);
            var game = _ongoingGameManager.GetGameById(id);
            var expectedGameResult = game;
            expectedGameResult.HomeTeam.Score = 1;
            _scoreBoard.UpdateGameScore(id, 1, 0);

            // Assert
            Assert.Equal(game, expectedGameResult);
        }
    }
}
