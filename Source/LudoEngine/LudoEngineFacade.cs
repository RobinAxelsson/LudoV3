using System;
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
            StaticBoard.BoardSquares = GameSquareFactory.CreateGameSquares(@"LudoORM/Map/BoardMap.txt");
            GameSetup.SetUpPawnsNewGame(StaticBoard.BoardSquares, System.Enum.GetValues<TeamColorCore>());
        }

        public static List<IGameSquare> GetNewGameBoardSquares()
        {
            var squares = GameSquareFactory.CreateGameSquares(@"LudoORM/Map/BoardMap.txt");
            GameSetup.SetUpPawnsNewGame(squares);
            return squares;
        }
    }

    public record GameDto(int Id, DateTime LastSaved);
}
