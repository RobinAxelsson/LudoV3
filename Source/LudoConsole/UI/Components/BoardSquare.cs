using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Components
{
    internal class BoardSquare : BoardSquareBase
    {
        public BoardSquare(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<DrawableBase> Refresh()
        {
            if (!Pawns.Any()) return CreateSquareDrawablesWithoutPawns();

            var squareDrawables = CreateSquareDrawablesWithoutPawns();
            var pawnDrawables = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(squareDrawables, pawnDrawables);

            return squareDrawables;
        }

        private List<DrawableBase> CreateSquareDrawablesWithoutPawns()
        {
            var drawables = new List<DrawableBase>();

            foreach (var charCoord in CharPoints)
            {
                drawables.Add(new DrawableSquare(charCoord.Char, (charCoord.X, charCoord.Y), Color));
            }

            return drawables;
        }
    }
}