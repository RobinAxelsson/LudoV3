using LudoEngine.BoardUnits.Interfaces;
using System.Collections.Generic;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Interfaces
{
    public interface ISquareDrawable
    {
        public ConsoleGameSquare Square { get; set; }
        public (int X, int Y) MaxCoord();
        public List<IDrawable> Refresh();
    }
}