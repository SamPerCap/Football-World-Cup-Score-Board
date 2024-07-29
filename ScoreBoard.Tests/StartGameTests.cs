using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Tests
{
    public class StartGameTests
    {
        private readonly ScoreBoard _scoreBoard;

        public StartGameTests()
        {
            _scoreBoard = new ScoreBoard();
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
            List<Game> ongoingGames = _scoreBoard.GetOnGoingGames();
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
            Assert.Single(_scoreBoard.GetOnGoingGames());
        }
    }
}