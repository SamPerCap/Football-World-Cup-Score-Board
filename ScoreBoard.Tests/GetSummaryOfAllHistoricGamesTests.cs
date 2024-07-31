using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;

namespace ScoreBoardLibrary.Tests
{
    public class GetSummaryOfAllHistoricGamesTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public GetSummaryOfAllHistoricGamesTests()
        {
            _teamManager = new TeamManager();
            _gameRepository = new GameRepository();
            _ongoingGameManager = new OngoingGameManager(_gameRepository);
            _finishedGameManger = new FinishedGameManager(_gameRepository);
            _gameManager = new GameManager(_gameRepository, _teamManager, _ongoingGameManager, _finishedGameManger);

            _scoreBoard = new ScoreBoard(
                _ongoingGameManager,
                _finishedGameManger,
                _gameRepository,
                _gameManager
            );
        }

        [Fact]
        public void ShouldReturnListWhenOnlyOngoing()
        {
            // Arrange
            _scoreBoard.StartGame("Ongoing Team A", "Ongoing Team B");
            _scoreBoard.StartGame("Ongoing Team C", "Ongoing Team D");
            _scoreBoard.StartGame("Ongoing Team E", "Ongoing Team F");

            // Act
            var summary = _scoreBoard.GetSummaryOfAllHistoricGames();

            // Assert
            Assert.Equal(3, summary.Count);
        }

        [Fact]
        public void ShouldReturnListWhenOnlyFinished()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Ongoing Team A", "Ongoing Team B");
            var secondGameId = _scoreBoard.StartGame("Ongoing Team C", "Ongoing Team D");

            // Act
            _scoreBoard.FinishGame(firstGameId);
            _scoreBoard.FinishGame(secondGameId);

            var summary = _scoreBoard.GetSummaryOfAllHistoricGames();

            // Assert
            Assert.Equal(2, summary.Count);
        }

        [Fact]
        public void ShouldReturnListWithBothLists()
        {
            // Arrange
            _scoreBoard.StartGame("Ongoing Team A", "Ongoing Team B");

            var finishedGameId = _scoreBoard.StartGame("Finished Team A", "Finished Team B");
            _scoreBoard.FinishGame(finishedGameId);

            // Act
            var summary = _scoreBoard.GetSummaryOfAllHistoricGames();

            // Assert
            Assert.Equal(2, summary.Count);
        }
    }
}
