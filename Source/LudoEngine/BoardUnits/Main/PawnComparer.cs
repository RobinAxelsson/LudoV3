using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.BoardUnits.Main
{
    public class PawnComparer : IEqualityComparer<Pawn>
    {
        public bool Equals(Pawn x, Pawn y) => x.Id == y.Id;
        public int GetHashCode(Pawn obj) => obj.GetHashCode();
    }
}
