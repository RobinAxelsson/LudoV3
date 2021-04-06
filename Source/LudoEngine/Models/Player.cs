using System.Collections.Generic;

namespace LudoEngine.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public virtual ICollection<Game> Game { get; set; }
    }
}