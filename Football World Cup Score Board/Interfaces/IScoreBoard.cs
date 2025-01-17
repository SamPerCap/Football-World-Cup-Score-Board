﻿using ScoreBoardLibrary.Models;

namespace ScoreBoardLibrary.Interfaces
{
    public interface IScoreBoard
    {
        Guid StartGame(string homeTeam, string awayTeam);

        void FinishGame(Guid gameId);

        void UpdateGameScore(Guid gameId, int homeTeamScore, int awayTeamScore);

        List<Game> GetSummaryOfOngoingGames();

        List<Game> GetSummaryOfFinishedGames();

        List<Game> GetSummaryOfAllHistoricGames();

        List<Game> GetSummaryOfGamesByDate(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
