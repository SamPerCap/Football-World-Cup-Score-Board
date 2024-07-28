using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary
{
    public class ScoreBoard : IScoreBoard
    {
        public void StartGame(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam)) {
                throw new ArgumentException("Team names have not been provided");
            }
        }

        public void FinishGame()
        {
            throw new NotImplementedException();
        }

        public void GetSummaryOfGames()
        {
            throw new NotImplementedException();
        }

        public void UpdateGame()
        {
            throw new NotImplementedException();
        }
    }
}
