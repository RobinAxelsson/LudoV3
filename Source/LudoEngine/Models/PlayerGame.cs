using System.Collections.Generic;

namespace LudoEngine.Models
{
    internal class PlayerGame
    {
        public int PlayerId { get; set; }
        public ICollection<Player> Player { get; set; }
        public int GameId { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}