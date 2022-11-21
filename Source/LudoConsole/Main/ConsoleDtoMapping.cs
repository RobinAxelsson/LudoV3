using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    internal static class ConsoleDtoMapping
    {
        public static IEnumerable<ConsoleGameSquare> Map(IEnumerable<IGameSquare> gameSquares)
        {
            return gameSquares.Select(x => MapSingle(x));
        }

        private static ConsoleGameSquare MapSingle(IGameSquare square)
        {
            return new ConsoleGameSquare()
            {
                IsBase = square.GetType().Name == "BaseSquare",
                BoardX = square.BoardX,
                BoardY = square.BoardY,
                Color = MapColor(square.Color),
                Pawns = square.Pawns.Select(x => MapPawn(x)).ToList(),
            };
        }

        private static ConsolePawnDto MapPawn(Pawn pawn)
        {
            return new ConsolePawnDto()
            {
                Id = pawn.Id,
                IsSelected = pawn.IsSelected,
                Color = MapColor(pawn.Color),
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
    }
}