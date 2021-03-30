using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Interfaces
{
    public interface ISquare
    {
        //public int Index { get; set; }
        public (int X, int Y) UpperLeft { get; set; }
        public List<IDrawable> Drawables { get; set; }
        public List<Pawn> Pawns { get; set; }
    }
}
