using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Models
{
    public class GameState
    {
        public int Id { get; set; }
        public virtual ICollection<Player> Players { get; set; } 
        public int CurrentPlayer { get; set; }
        public virtual ICollection<Pawn> Pawns { get; set; }
    }
}