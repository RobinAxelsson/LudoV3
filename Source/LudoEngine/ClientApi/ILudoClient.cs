using System;
using LudoEngine.BoardUnits.Interfaces;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;

namespace LudoEngine.ClientApi
{
    public interface ILudoClient
    {
        //gameSquares Dtos
        public void OnNewGame(DtoGame dtoGame);

        //pawnDtos
        public void OnReceiveUpdatedPawns(IEnumerable<DtoPawn> pawnDtos);

        //pawnDto
        public DtoPawn OnMakeMove(DtoMakeMove dtoMakeMove);


    }

    public enum SquareType
    {
        Exit,
        Goal,
        SafeZone,
        Standard,
        Start,
        TeamBase
    }

    //public enum TeamColor

    internal static class ClientMapper
    {
        public static DtoGameSquare MapGameSquare(IGameSquare gameSquare)
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

            return new DtoGameSquare(gameSquare.BoardX, gameSquare.BoardY, gameSquare.Color, GetSquareType(gameSquare));
        }

        public static DtoPawn MapPawn(Pawn pawn)
        {
            return new DtoPawn(pawn.Id, pawn.CurrentSquare().BoardX, pawn.CurrentSquare().BoardY, pawn.Color);
        }
    }

}

