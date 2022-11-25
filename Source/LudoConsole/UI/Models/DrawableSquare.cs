using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Controls;
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

        private List<IDrawable> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Pawns;
            var drawables = new List<IDrawable>();
            var pawnColor = UiColor.TranslateColor(Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                PawnDrawable newPawn;

                newPawn = pawns[i].IsSelected 
                    ? new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), Color) 
                    : new PawnDrawable(PawnCoords[i], pawnColor, Color);

                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), Color);

                drawables.Add(newPawn);
                drawables.Add(dropShadow);
            }

            return drawables;
        }

        private static void AddPawnDrawablesToSquareDrawables(List<IDrawable> drawablesWithOutPawns, IEnumerable<IDrawable> pawnDrawables)
        {
            var pawnXYs = pawnDrawables.Select(x => (x.CoordinateX, x.CoordinateY));
            drawablesWithOutPawns.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            drawablesWithOutPawns.AddRange(pawnDrawables);
        }
    }
}