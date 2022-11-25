using System;
using System.Collections.Generic;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Interfaces
{
    internal abstract class DrawableSquareBase
    {
        public ConsoleGameSquare Square { get; }
        public abstract (int X, int Y) MaxCoord();
        public abstract List<IDrawable> Refresh();
        public ConsoleColor ThisBackgroundColor() => UiColor.TranslateColor(Square.Color);

        protected List<CharPoint> CharPoints { get; }
        protected List<(int X, int Y)> PawnCoords { get; }

        protected DrawableSquareBase(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, ConsoleGameSquare square)
        {
            CharPoints = charPoints;
            PawnCoords = pawnCoords;
            Square = square;
        }
    }
}