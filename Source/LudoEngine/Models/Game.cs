using System.Collections.Generic;

namespace LudoEngine.Models
{
    public class Game
    {
        public int Id { get; set; }
        public virtual ICollection<Player> Player { get; set; }

        public string CurrentTurn { get; set; }
        public int FirstPlace { get; set; }
        public int SecondPlace { get; set; }
        public int ThirdPlace { get; set; }
        public int ForthPlace { get; set; }
    }
}