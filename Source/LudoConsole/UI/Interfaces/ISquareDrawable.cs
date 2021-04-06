using LudoEngine.BoardUnits.Intefaces;
using System.Collections.Generic;

namespace LudoConsole.UI.Models
{
    public interface ISquareDrawable
    {
        public IGameSquare Square { get; set; }
        public (int X, int Y) MaxCoord();
        public List<IDrawable> Refresh();
    }
}