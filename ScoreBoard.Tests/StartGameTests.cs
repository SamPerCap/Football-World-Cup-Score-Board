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
    }
}