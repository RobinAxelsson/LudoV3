using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Mapping;
using LudoConsole.Ui.Models;

namespace LudoConsole.Ui.Components
{
    internal class UiGameSquare : UiGameSquareBase
    {
        public UiGameSquare(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns,
            ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<DrawableCharPoint> Refresh()
        {
            if (!Pawns.Any()) return CreateSquareDrawablesWithoutPawns();

            var squareDrawables = CreateSquareDrawablesWithoutPawns();
            var pawnDrawables = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(squareDrawables, pawnDrawables);

            return squareDrawables;
        }

        private List<DrawableCharPoint> CreateSquareDrawablesWithoutPawns()
        {
            var drawables = new List<DrawableCharPoint>();

            foreach (var charCoord in CharPoints)
                drawables.Add(DrawableCharPoint.Square(charCoord.Char, (charCoord.X, charCoord.Y), Color));

            return drawables;
        }
    }
}