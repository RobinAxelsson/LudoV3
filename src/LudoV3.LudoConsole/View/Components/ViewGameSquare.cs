using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Model;
using LudoConsole.View.Components.Models;

namespace LudoConsole.View.Components
{
    internal sealed class ViewGameSquare : ViewGameSquareBase
    {
        public ViewGameSquare(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawn> consolePawns,
            ConsoleColor color) : base(charPoints, pawnCoords, consolePawns, color)
        {
        }

        public override List<ConsolePixel> Refresh()
        {
            if (!ConsolePawns.Any()) return CreateViewGameSquareConsolePixels();

            var squarePixels = CreateViewGameSquareConsolePixels();
            var pawnPixels = CreatePawnConsolePixelsWithDropShadow();
            AddPawnPixelsToSquarePixels(squarePixels, pawnPixels);

            return squarePixels;
        }

        private List<ConsolePixel> CreateViewGameSquareConsolePixels()
        {
            var consolePixels = new List<ConsolePixel>();

            foreach (var charPoint in CharPoints)
                consolePixels.Add(ConsolePixel.Square(charPoint.Char, (charPoint.X, charPoint.Y), Color));

            return consolePixels;
        }
    }
}