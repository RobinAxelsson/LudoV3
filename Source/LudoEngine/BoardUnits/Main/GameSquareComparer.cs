using LudoEngine.BoardUnits.Intefaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.BoardUnits.Interfaces
{
    public class GameSquareComparer : IEqualityComparer<IGameSquare>
    {
        public bool Equals(IGameSquare x, IGameSquare y) => x.BoardX == y.BoardX && x.BoardY == y.BoardY;
        public int GetHashCode(IGameSquare obj) => obj.GetHashCode();
    }
}
