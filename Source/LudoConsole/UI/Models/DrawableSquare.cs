using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    internal class DrawableSquare : DrawableSquareBase
    {
        public DrawableSquare(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<IDrawable> Refresh()
        {
            if (!Pawns.Any()) return CreateSquareDrawablesWithoutPawns();
            
            var squareDrawables = CreateSquareDrawablesWithoutPawns();
            var pawnDrawables = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(squareDrawables, pawnDrawables);

            return squareDrawables;
        }

        private List<IDrawable> CreateSquareDrawablesWithoutPawns()
        {
            var drawables = new List<IDrawable>();

            foreach (var charCoord in CharPoints)
            {
                drawables.Add(new LudoDrawable(charCoord.Char, (charCoord.X, charCoord.Y), Color));
            }

            return drawables;
        }
    }
}