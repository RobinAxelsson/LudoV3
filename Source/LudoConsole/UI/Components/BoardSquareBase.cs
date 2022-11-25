using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Components
{
    internal abstract class BoardSquareBase
    {
        public (int X, int Y) MaxCoord() => CharPoints.Select(x => (x.X, x.Y)).Max(x => (x.X, x.Y));
        protected ConsoleColor Color { get; }
        protected List<CharPoint> CharPoints { get; }
        protected List<(int X, int Y)> PawnCoords { get; }
        protected List<ConsolePawnDto> Pawns { get; }

        protected BoardSquareBase(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color)
        {
            CharPoints = charPoints;
            PawnCoords = pawnCoords;
            this.Pawns = Pawns;
            Color = color;
        }

        public abstract List<DrawableBase> Refresh();

        protected void AddPawnDrawablesToSquareDrawables(List<DrawableBase> pawnDraws, List<DrawableBase> squareDrawables)
        {
            var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
            var count = squareDrawables.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            if (count != Pawns.Count * 2) throw new Exception($"Removed {count}");
            squareDrawables.AddRange(pawnDraws);
        }

        protected List<DrawableBase> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Pawns;
            var drawables = new List<DrawableBase>();
            var pawnColor = UiColor.TranslateColor(Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                var newPawn = pawns[i].IsSelected
                    ? new DrawablePawn(PawnCoords[i], UiColor.RandomColor(), Color)
                    : new DrawablePawn(PawnCoords[i], pawnColor, null);

                var dropShadow = new DrawableSquare('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), UiColor.LightAccent);

                drawables.Add(newPawn);
                drawables.Add(dropShadow);
            }

            return drawables;
        }
    }
}