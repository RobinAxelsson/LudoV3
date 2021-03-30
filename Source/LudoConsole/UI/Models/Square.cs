using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoConsole.UI.Interfaces
{
    public class Square : ISquare
    {
        public int Index { get; set; }
        public (int X, int Y) UpperLeft { get; set; }
        public List<IDrawable> Drawables { get; set; }
        public List<Pawn> Pawns { get; set; }
        public List<IDrawable> UpdatePawns() { return new List<IDrawable>(); }
    }
}
