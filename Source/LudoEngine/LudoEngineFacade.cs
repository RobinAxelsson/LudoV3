﻿using System;
using LudoEngine.DbModel;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.BoardUnits.Interfaces;

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

        public static void SetBoard()
        {
            StaticBoard.BoardSquares = BoardOrm.Map(@"LudoORM/Map/BoardMap.txt");
            GameSetup.NewGame(StaticBoard.BoardSquares, System.Enum.GetValues<TeamColor>());
        }

        public static List<IGameSquare> GetBoardSquares()
        {
            return BoardOrm.Map(@"LudoORM/Map/BoardMap.txt");
        }

        //public static void AddPlayer()
    }

    public record GameDto(int Id, DateTime LastSaved);
}
