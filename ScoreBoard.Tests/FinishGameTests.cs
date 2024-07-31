using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;

namespace ScoreBoardLibrary.Tests
{
    public class FinishGameTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public FinishGameTests()
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
        public void ShouldThrowExceptionIfGameNotFound()
        {
            Assert.Throws<InvalidOperationException>(() => _scoreBoard.FinishGame(Guid.NewGuid()));
        }


        [Fact]
        public void ShouldFinishTheChoosenGame()
        {
            // Arrange
            string homeTeam = "Home Team";
            string awayTeam = "Away Team";
            string homeTeamB = "Home Team B";
            string awayTeamB = "Away Team B";

            // Act
            var id = _scoreBoard.StartGame(homeTeam, awayTeam);
            _scoreBoard.StartGame(homeTeamB, awayTeamB);

            _scoreBoard.FinishGame(id);

            // Assert
            Assert.Single(_scoreBoard.GetSummaryOfFinishedGames());
            Assert.Single(_scoreBoard.GetSummaryOfOngoingGames());
        }

        [Fact]
        public void ShouldMarkGameAsFinished()
        {
            // Arrange
            string homeTeam = "Home Team";
            string awayTeam = "Away Team";
            var id = _scoreBoard.StartGame(homeTeam, awayTeam);

            // Act
            _scoreBoard.FinishGame(id);
            var finishedGame = _scoreBoard.GetSummaryOfFinishedGames().Single(game => game.Id == id);

            // Assert
            Assert.True(finishedGame.IsFinished);
        }
    }
}
