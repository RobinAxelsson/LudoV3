using LudoEngine.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public int ID { get; set; }
        public int GameID  { get; set; }
        public TeamColor Color { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public virtual Game game { get; set; }
    }
}