using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Interfaces.GameManagement;

namespace ScoreBoardLibrary.Tests
{
    public class GetSummaryOfFinishedGamesTests
    {
        private readonly IScoreBoard _scoreBoard;
        private readonly ITeamManager _teamManager;
        private readonly IGameRepository _gameRepository;
        private readonly IOngoingGameManager _ongoingGameManager;
        private readonly IFinishedGameManager _finishedGameManger;
        private readonly IGameManager _gameManager;

        public GetSummaryOfFinishedGamesTests()
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
        public void ShouldReturnEmptyIfNotFinished()
        {
            Assert.Empty(_scoreBoard.GetSummaryOfFinishedGames());
        }

        [Fact]
        public void ShouldReturnCorrectFinishedList()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");

            _scoreBoard.FinishGame(firstGameId);
            _scoreBoard.FinishGame(secondGameId);

            // Act
            var summary = _scoreBoard.GetSummaryOfFinishedGames();

            // Assert
            Assert.Equal(2, summary.Count);
        }

        [Fact]
        public void ShouldReturnListOrderedByScore()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            _scoreBoard.UpdateGameScore(firstGameId, 1, 0);
            _scoreBoard.FinishGame(firstGameId);

            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");
            _scoreBoard.UpdateGameScore(secondGameId, 2, 2);
            _scoreBoard.FinishGame(secondGameId);

            var thirdGameId = _scoreBoard.StartGame("Team E", "Team F");
            _scoreBoard.UpdateGameScore(thirdGameId, 0, 2);
            _scoreBoard.FinishGame(thirdGameId);

            // Act
            var summary = _scoreBoard.GetSummaryOfFinishedGames();

            // Assert
            Assert.Equal(summary[0], _finishedGameManger.GetGameById(secondGameId));
            Assert.Equal(summary[1], _finishedGameManger.GetGameById(thirdGameId));
            Assert.Equal(summary[2], _finishedGameManger.GetGameById(firstGameId));
        }

        [Fact]
        public void ShouldReturnListOrderedByScoreAndDate()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            _scoreBoard.UpdateGameScore(firstGameId, 1, 3);
            _scoreBoard.FinishGame(firstGameId);

            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");
            _scoreBoard.UpdateGameScore(secondGameId, 2, 2);
            _scoreBoard.FinishGame(secondGameId);

            var thirdGameId = _scoreBoard.StartGame("Team E", "Team F");
            _scoreBoard.UpdateGameScore(thirdGameId, 0, 2);
            _scoreBoard.FinishGame(thirdGameId);

            // Act
            var summary = _scoreBoard.GetSummaryOfFinishedGames();

            // Assert
            Assert.True(
                summary[0].TotalScore > summary[1].TotalScore ||
                (summary[0].TotalScore == summary[1].TotalScore && summary[0].Audit.Created >= summary[1].Audit.Created)
            );

            Assert.True(
                summary[1].TotalScore > summary[2].TotalScore ||
                (summary[1].TotalScore == summary[2].TotalScore && summary[1].Audit.Created >= summary[2].Audit.Created)
            );
        }

        [Fact]
        public void ShouldReturnEmptyListFinishedAndThenCompleted()
        {
            // Arrange
            var firstGameId = _scoreBoard.StartGame("Team A", "Team B");
            var secondGameId = _scoreBoard.StartGame("Team C", "Team D");

            _scoreBoard.UpdateGameScore(firstGameId, 1, 0);
            
            // Assert
            Assert.Empty(_scoreBoard.GetSummaryOfFinishedGames());

            // Act
            _scoreBoard.FinishGame(firstGameId);
            _scoreBoard.FinishGame(secondGameId);

            // Assert
            Assert.Equal(2, _scoreBoard.GetSummaryOfFinishedGames().Count);
        }
    }
}
