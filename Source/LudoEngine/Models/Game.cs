using System.Collections.Generic;

namespace LudoEngine.Models
{
    public class Game
    {
        public Game()
        {
            this.Players = new HashSet<Player>();
        }
        public int Id { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public string CurrentTurn { get; set; }
        public int FirstPlace { get; set; }
        public int SecondPlace { get; set; }
        public int ThirdPlace { get; set; }
        public int FourthPlace { get; set; }
        public virtual Pawn pawn { get; set; }
    }
}