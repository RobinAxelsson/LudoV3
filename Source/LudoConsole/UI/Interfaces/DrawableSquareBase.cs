using System;
using System.Collections.Generic;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Interfaces
{
    public abstract class DrawableSquareBase
    {
        public ConsoleGameSquare Square { get; }
        public abstract (int X, int Y) MaxCoord();
        public abstract List<IDrawable> Refresh();
        public ConsoleColor ThisBackgroundColor() => UiColor.TranslateColor(Square.Color);
        protected DrawableSquareBase(ConsoleGameSquare square)
        {
            Square = square;
        }
    }
}