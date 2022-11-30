using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Enums;
using LudoConsole.Model;
using LudoEngine.ClientApi.Dto;
using LudoEngine.ClientApi.Enums;

namespace LudoConsole.LudoEngine
{
    internal static class LudoEngineMapper
    {
        public static IEnumerable<ConsoleGameSquare> Map(DtoLudoGame ludoGame)
        {
            return ludoGame.GameSquares.Select(MapGameSquare);
        }

        private static ConsoleGameSquare MapGameSquare(DtoGameSquare square)
        {
            return new ConsoleGameSquare
            {
                IsBase = square.SquareType == SquareType.TeamBase,
                BoardX = square.BoardX,
                BoardY = square.BoardY,
                Color = MapColor(square.Color),
                Pawns = square.Pawns.Select(MapPawn).ToList(),
            };
        }

        private static ConsolePawn MapPawn(DtoPawn pawn)
        {
            return new ConsolePawn
            {
                Id = pawn.Id,
                //IsSelected = true,
                IsSelected = false,
                Color = MapColor(pawn.Color)
            };
        }

        private static ConsoleTeamColor MapColor(LudoColor color)
        {
            return color switch
            {
                LudoColor.Blue => ConsoleTeamColor.Blue,
                LudoColor.Red => ConsoleTeamColor.Red,
                LudoColor.Yellow => ConsoleTeamColor.Yellow,
                LudoColor.Green => ConsoleTeamColor.Green,
                LudoColor.Default => ConsoleTeamColor.Default,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }
    }
}