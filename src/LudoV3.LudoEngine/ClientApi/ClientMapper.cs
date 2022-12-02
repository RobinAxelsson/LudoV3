using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.Board.Square;
using LudoEngine.ClientApi.Dto;
using LudoEngine.ClientApi.Enums;
using LudoEngine.Enums;
using LudoEngine.GameLogic;

namespace LudoEngine.ClientApi
{
    internal static class ClientMapper
    {
        public static DtoGameBoard MapDtoGameBoard(List<GameSquareBase> boardSquares)
        {
            return new DtoGameBoard(boardSquares.Select(MapGameSquare));
        }

        private static DtoGameSquare MapGameSquare(GameSquareBase gameGameSquare)
        {
            SquareType GetSquareType(GameSquareBase square)
            {
                return square switch
                {
                    GameSquareExit => SquareType.Exit,
                    GameSquareGoal => SquareType.Goal,
                    GameSquareSafeZone => SquareType.SafeZone,
                    GameSquareStandard => SquareType.Standard,
                    GameSquareStart  => SquareType.Start,
                    GameSquareTeamBase => SquareType.TeamBase,
                    _ => throw new ArgumentOutOfRangeException(nameof(square))
                };
            }

            return new DtoGameSquare(gameGameSquare.BoardX, gameGameSquare.BoardY, MapTeamColor(gameGameSquare.Color), GetSquareType(gameGameSquare), MapPawns(gameGameSquare.Pawns));
        }

        private static IEnumerable<DtoPawn> MapPawns(IEnumerable<Pawn> pawns)
        {
            return pawns.Select(pawn => new DtoPawn(pawn.Id, pawn.CurrentSquare().BoardX, pawn.CurrentSquare().BoardY, MapTeamColor(pawn.Color)));
        }

        private static LudoColor MapTeamColor(TeamColor? teamColorCore)
        {
            return teamColorCore switch
            {
                TeamColor.Blue => LudoColor.Blue,
                TeamColor.Red => LudoColor.Red,
                TeamColor.Yellow => LudoColor.Yellow,
                TeamColor.Green => LudoColor.Green,
                null => LudoColor.Default,
                _ => throw new ArgumentOutOfRangeException(nameof(teamColorCore), teamColorCore, null)
            };
        }
    }
}