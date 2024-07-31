using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;

namespace ScoreBoardLibrary.Tests
{
    public class GetSummaryOfAllGamesByDateTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public GetSummaryOfAllGamesByDateTests()
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
        public void ShouldReturnListWhenDateRangeIncludesGames()
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(-1);
            var endDate = DateTimeOffset.UtcNow.AddDays(1);

            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            _scoreBoard.FinishGame(firstGameId);

            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");

            // Act
            var summary = _scoreBoard.GetSummaryOfGamesByDate(startDate, endDate);

            // Assert
            Assert.Equal(2, summary.Count);
        }

        [Fact]
        public void ShouldReturnEmptyListWhenNoGamesInDateRange()
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(-1);
            var endDate = DateTimeOffset.UtcNow.AddDays(1);

            // Act
            var summary = _scoreBoard.GetSummaryOfGamesByDate(startDate, endDate);

            // Assert
            Assert.Empty(summary);
        }

        [Fact]
        public void ShouldIncludeGamesOnExactStartAndEndDates()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");
            
            var startDate = DateTimeOffset.UtcNow.AddDays(-1);
            var endDate = DateTimeOffset.UtcNow;

            _scoreBoard.FinishGame(secondGameId);

            // Act
            var summary = _scoreBoard.GetSummaryOfGamesByDate(startDate, endDate);

            // Assert
            Assert.Equal(2, summary.Count);
        }

        [Fact]
        public void ShouldHandleStartDateAfterEndDate()
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(1);
            var endDate = DateTimeOffset.UtcNow;

            // Assert
            Assert.Throws<ArgumentException>(() => _scoreBoard.GetSummaryOfGamesByDate(startDate, endDate));
        }

        [Fact]
        public void ShouldHandleFutureDates()
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(1);
            var endDate = DateTimeOffset.UtcNow.AddDays(2);

            // Act
            var summary = _scoreBoard.GetSummaryOfGamesByDate(startDate, endDate);

            // Assert
            Assert.Empty(summary);
        }
    }
}
