using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Tests
{
    public class GetSummaryOfAllGamesByDateTests
    {
        private readonly IScoreBoard _scoreBoard;

        public GetSummaryOfAllGamesByDateTests()
        {
            _scoreBoard = new ScoreBoard();
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
