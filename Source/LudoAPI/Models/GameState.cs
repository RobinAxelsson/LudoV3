using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class GameState
    {
        public List<int> Players { get; set; }
        public int CurrentPlayer { get; set; }
        public List<Pawn> Pawns { get; set; }
    }
}
