using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Tests
{
    public class FinishGameTests
    {
        private readonly IScoreBoard _scoreBoard;

        public FinishGameTests()
        {
            _scoreBoard = new ScoreBoard();
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
            Assert.Single(_scoreBoard.GetFinishedGames());
            Assert.Single(_scoreBoard.GetAllOngoingGames());
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
            var finishedGame = _scoreBoard.GetFinishedGames().Single(game => game.Id == id);

            // Assert
            Assert.True(finishedGame.IsFinished);
        }
    }
}
