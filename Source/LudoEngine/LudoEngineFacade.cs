using System;
using LudoEngine.DbModel;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine
{
    public static class LudoEngineFacade
    {
        public static IReadOnlyList<GameDto> GetSavedGames()
        {
            var games = DatabaseManagement.GetGames();
            return games.Select(x => new GameDto(x.Id, x.LastSaved)).ToArray();
        }

        public static StageSavingDto GetStageSavingDto(int id)
        {
            var game = DatabaseManagement.GetGames().Single(x =>x.Id == id);

            return new StageSavingDto()
            {
                CurrentTeam = StageSaving.CurrentTeam,
                Pawns = StageSaving.Pawns,
                Game = game,
                Players = StageSaving.Players,
                TeamPosition = DatabaseManagement.GetPawnPositionsInGame(game)
            };
        }
    }

    public record GameDto(int Id, DateTime LastSaved);
}
