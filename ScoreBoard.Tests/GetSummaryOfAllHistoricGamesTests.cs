using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Tests
{
    public class GetSummaryOfAllHistoricGamesTests
    {
        private readonly IScoreBoard _scoreBoard;

        public GetSummaryOfAllHistoricGamesTests()
        {
            _scoreBoard = new ScoreBoard();
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
