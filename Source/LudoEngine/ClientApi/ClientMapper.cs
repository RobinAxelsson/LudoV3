using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.Board.Square;
using LudoEngine.ClientApi.Dto;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.ClientApi
{
    internal static class ClientMapper
    {
        public static DtoLudoGame MapGame(List<IGameSquare> boardSquares)
        {
            return new DtoLudoGame(boardSquares.Select(MapGameSquare));
        }

        private static DtoGameSquare MapGameSquare(IGameSquare gameSquare)
        {
            SquareType GetSquareType(IGameSquare square)
            {
                return square switch
                {
                    SquareExit => SquareType.Exit,
                    SquareGoal => SquareType.Goal,
                    SquareSafeZone => SquareType.SafeZone,
                    SquareStandard => SquareType.Standard,
                    SquareStart  => SquareType.Start,
                    SquareTeamBase => SquareType.TeamBase,
                    _ => throw new ArgumentOutOfRangeException(nameof(square))
                };
            }

            return new DtoGameSquare(gameSquare.BoardX, gameSquare.BoardY, MapTeamColor(gameSquare.Color), GetSquareType(gameSquare), MapPawns(gameSquare.Pawns));
        }

        private static IEnumerable<DtoPawn> MapPawns(IEnumerable<Pawn> pawns)
        {
            return pawns.Select(pawn => new DtoPawn(pawn.Id, pawn.CurrentSquare().BoardX, pawn.CurrentSquare().BoardY, pawn.Color));
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