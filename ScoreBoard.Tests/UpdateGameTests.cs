using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Tests
{
    public class UpdateGameTests
    {
        private readonly IScoreBoard _scoreBoard;

        public UpdateGameTests()
        {
            _scoreBoard = new ScoreBoard();
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
            Assert.Throws<InvalidOperationException>(() => _scoreBoard.UpdateGame(Guid.NewGuid(), 1, 0));
        }

        [Fact]
        public void ShouldUpdateTheCorrectGameOnce()
        {
            // Arrange
            string homeTeam = "Home Team";
            string awayTeam = "Away Team";

            // Act
            var id = _scoreBoard.StartGame(homeTeam, awayTeam);
            var game = _scoreBoard.GetOngoingGameById(id);
            var expectedGameResult = game;
            expectedGameResult.HomeTeam.Score = 1;
            _scoreBoard.UpdateGame(id, 1, 0);

            // Assert
            Assert.Equal(game, expectedGameResult);
        }
    }
}
