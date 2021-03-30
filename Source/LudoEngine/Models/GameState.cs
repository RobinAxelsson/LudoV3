using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.Models
{
    public class GameState
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public Player Player { get; set; }
        public int CurrentPlayer { get; set; }
        public Pawn Pawn { get; set; }
    }
}