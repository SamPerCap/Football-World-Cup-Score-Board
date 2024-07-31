namespace ScoreBoardLibrary.Interfaces
{
    public interface ITeamManager
    {
        bool IsTeamPlaying(string teamName);
        void AddTeam(string teamName);
        void RemoveTeam(string teamName);
    }
}
