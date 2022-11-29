using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.ClientApi.Dto;

namespace LudoConsole.ServerMapping
{
    internal static class ConsoleDtoMapper
    {
        public static IEnumerable<DtoConsoleGameSquare> Map(DtoLudoGame ludoGame)
        {
            return ludoGame.GameSquares.Select(MapGameSquare);
        }

        private static DtoConsoleGameSquare MapGameSquare(DtoGameSquare square)
        {
            return new DtoConsoleGameSquare
            {
                IsBase = square.SquareType == SquareType.TeamBase,
                BoardX = square.BoardX,
                BoardY = square.BoardY,
                Color = MapColor(square.Color),
                Pawns = square.Pawns.Select(MapPawn).ToList(),
            };
        }

        private static DtoConsolePawn MapPawn(DtoPawn pawn)
        {
            return new DtoConsolePawn
            {
                Id = pawn.Id,
                IsSelected = true,
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