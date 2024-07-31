using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary
{
    public class TeamManager : ITeamManager
    {
        private readonly HashSet<string> _teamsPlaying;

        public TeamManager()
        {
            _teamsPlaying = new HashSet<string>();
        }

        public bool IsTeamPlaying(string teamName)
        {
            return _teamsPlaying.Contains(teamName);
        }

        public void AddTeam(string teamName)
        {
            _teamsPlaying.Add(teamName);
        }

        public void RemoveTeam(string teamName)
        {
            _teamsPlaying.Remove(teamName);
        }
    }
}
