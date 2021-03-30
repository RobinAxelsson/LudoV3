using System.Collections.Generic;

namespace LudoEngine.Models
{
    public class GameState
    {
        public int Id { get; set; }
        public int GameID { get; set; }
        public virtual ICollection<Player> Player { get; set; }
        public int CurrentPlayer { get; set; }
        public Pawn Pawn { get; set; }
    }
}