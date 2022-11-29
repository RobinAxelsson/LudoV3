using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.ClientApi.Dto;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoConsole.Mapping
{
    internal static class ConsoleDtoMapping
    {
        public static IEnumerable<ConsoleGameSquare> Map(IEnumerable<IGameSquare> gameSquares)
        {
            return gameSquares.Select(MapGameSquare);
        }
        private static ConsoleGameSquare MapGameSquare(IGameSquare square)
        {
            return new ConsoleGameSquare
            {
                IsBase = square.GetType().Name == "SquareTeamBase",
                BoardX = square.BoardX,
                BoardY = square.BoardY,
                Color = MapColor(square.Color),
                Pawns = square.Pawns.Select(MapPawn).ToList()
            };
        }

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

        private static ConsolePawnDto MapPawn(Pawn pawn)
        {
            return new ConsolePawnDto
            {
                Id = pawn.Id,
                IsSelected = true,
                Color = MapColor(pawn.Color)
            };
        }

        private static ConsolePawnDto MapPawn(DtoPawn pawn)
        {
            return new ConsolePawnDto
            {
                Id = pawn.Id,
                IsSelected = true,
                Color = MapColor(pawn.Color)
            };
        }

        private static ConsoleTeamColor MapColor(TeamColor? color)
        {
            return color switch
            {
                null => ConsoleTeamColor.Default,
                TeamColor.Blue => ConsoleTeamColor.Blue,
                TeamColor.Red => ConsoleTeamColor.Red,
                TeamColor.Yellow => ConsoleTeamColor.Yellow,
                TeamColor.Green => ConsoleTeamColor.Green,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
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