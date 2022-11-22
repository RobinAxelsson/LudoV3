using System.Collections.Generic;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Interfaces
{
    public abstract class DrawableSquareBase
    {
        public ConsoleGameSquare Square { get; set; }
        public abstract (int X, int Y) MaxCoord();
        public abstract List<IDrawable> Refresh();
    }
}