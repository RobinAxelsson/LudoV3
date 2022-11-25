using System.Collections.Generic;
using System.Linq;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    public class DrawableSquare : DrawableSquareBase
    {
        private List<CharPoint> CharCoords { get; }
        private List<(int X, int Y)> PawnCoords { get; } = new();
        public override (int X, int Y) MaxCoord()
        {
            var x = CharCoords.Select(x => (x.X, x.Y)).Max(x => x.X);
            var y = CharCoords.Select(x => (x.X, x.Y)).Max(x => x.Y);
            return (x, y);
        }

        public DrawableSquare(ConsoleGameSquare square) : base(square)
        {
           var (charCoords, pawnCoords) = 
               LudoSquareFactory.CreateSquareCharPoints((square.BoardX, square.BoardY));
           CharCoords = charCoords;
           //CharCoords = LudoSquareFactory.MapToValueTuples(charCoords);
           PawnCoords = pawnCoords;
        }
        
        public override List<IDrawable> Refresh()
        {
            if (!Square.Pawns.Any()) return CreateSquareDrawablesWithoutPawns();

            var squareDrawables = CreateSquareDrawablesWithoutPawns();
            var pawnDrawables = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(squareDrawables, pawnDrawables);

            return squareDrawables;
        }


        private List<IDrawable> CreateSquareDrawablesWithoutPawns()
        {
            var drawables = new List<IDrawable>();

            foreach (var charCoord in CharCoords)
            {
                var color = ThisBackgroundColor();
                drawables.Add(new LudoDrawable(charCoord.Char, (charCoord.X, charCoord.Y), color));
            }

            return drawables;
        }

        private List<IDrawable> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Square.Pawns;
            var drawables = new List<IDrawable>();
            var pawnColor = UiColor.TranslateColor(Square.Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                PawnDrawable newPawn;

                newPawn = pawns[i].IsSelected 
                    ? new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), ThisBackgroundColor()) 
                    : new PawnDrawable(PawnCoords[i], pawnColor, ThisBackgroundColor());

                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), ThisBackgroundColor());

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