using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Mapping;
using LudoConsole.Ui.Models;

namespace LudoConsole.Ui.Components
{
    internal abstract class BoardSquareBase
    {
        protected BoardSquareBase(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords,
            List<ConsolePawnDto> Pawns, ConsoleColor color)
        {
            CharPoints = charPoints;
            PawnCoords = pawnCoords;
            this.Pawns = Pawns;
            Color = color;
        }

        protected ConsoleColor Color { get; }
        protected List<CharPoint> CharPoints { get; }
        protected List<(int X, int Y)> PawnCoords { get; }
        protected List<ConsolePawnDto> Pawns { get; }

        public (int X, int Y) MaxCoord()
        {
            return CharPoints.Select(x => (x.X, x.Y)).Max(x => (x.X, x.Y));
        }

        public abstract List<DrawableCharPoint> Refresh();

        protected void AddPawnDrawablesToSquareDrawables(List<DrawableCharPoint> pawnDraws,
            List<DrawableCharPoint> squareDrawables)
        {
            var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
            var count = squareDrawables.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            if (count != Pawns.Count * 2) throw new Exception($"Removed {count}");
            squareDrawables.AddRange(pawnDraws);
        }

        protected List<DrawableCharPoint> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Pawns;
            var drawables = new List<DrawableCharPoint>();
            var pawnColor = UiColor.TranslateColor(Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                var newPawn = pawns[i].IsSelected
                    ? DrawableCharPoint.Pawn(PawnCoords[i], UiColor.RandomColor(), Color)
                    : DrawableCharPoint.Pawn(PawnCoords[i], pawnColor, null);

                var dropShadow =
                    DrawableCharPoint.Square('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), UiColor.LightAccent);

                drawables.Add(newPawn);
                drawables.Add(dropShadow);
            }

            return drawables;
        }
    }
}