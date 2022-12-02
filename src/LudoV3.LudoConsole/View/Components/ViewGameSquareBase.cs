using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Model;
using LudoConsole.View.Components.Models;

namespace LudoConsole.View.Components
{
    internal abstract class ViewGameSquareBase
    {
        protected ViewGameSquareBase(
            IEnumerable<CharPoint> charPoints,
            IEnumerable<(int X, int Y)> pawnCoords,
            IEnumerable<ConsolePawn> consolePawns,
            ConsoleColor color)
        {
            CharPoints = charPoints.ToArray();
            PawnCoords = pawnCoords.ToArray();
            ConsolePawns = consolePawns.ToList();
            Color = color;
        }

        protected ConsoleColor Color { get; }
        protected CharPoint[] CharPoints { get; }
        protected (int X, int Y)[] PawnCoords { get; }
        protected List<ConsolePawn> ConsolePawns { get; }

        public (int X, int Y) MaxCoord()
        {
            return CharPoints.Select(x => (x.X, x.Y)).Max(x => (x.X, x.Y));
        }

        public abstract List<ConsolePixel> Refresh();

        protected void AddPawnPixelsToSquarePixels(List<ConsolePixel> pawnDraws,
            List<ConsolePixel> squareDrawables)
        {
            var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
            var count = squareDrawables.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            if (count != ConsolePawns.Count * 2) throw new Exception($"Removed {count}");
            squareDrawables.AddRange(pawnDraws);
        }

        protected List<ConsolePixel> CreatePawnConsolePixelsWithDropShadow()
        {
            var consolePixels = new List<ConsolePixel>();
            var pawnColor = ColorManager.TranslateColor(ConsolePawns[0].Color);

            for (var i = 0; i < ConsolePawns.Count; i++)
            {
                var newPawn = ConsolePawns[i].IsSelected
                    ? ConsolePixel.Pawn(PawnCoords[i], ColorManager.RandomColor(), Color)
                    : ConsolePixel.Pawn(PawnCoords[i], pawnColor, null);

                var dropShadow =
                    ConsolePixel.Square('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), ColorManager.LightAccent);

                consolePixels.Add(newPawn);
                consolePixels.Add(dropShadow);
            }

            return consolePixels;
        }
    }
}