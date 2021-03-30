using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoEngine.Enum;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public int PlayerID { get; set; }
        public TeamColor Color { get; set; }
        public (int X, int Y) Position { get; set; }
    }
}
