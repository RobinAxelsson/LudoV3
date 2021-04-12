using LudoEngine.BoardUnits.Interfaces;
using System.Collections.Generic;

namespace LudoConsole.UI.Interfaces
{
    public interface ISquareDrawable
    {
        public IGameSquare Square { get; set; }
        public (int X, int Y) MaxCoord();
        public List<IDrawable> Refresh();
    }
}