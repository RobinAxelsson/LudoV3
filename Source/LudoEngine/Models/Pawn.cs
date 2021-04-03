using LudoEngine.Enum;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public Pawn(TeamColor color)
        {
            Color = color;
        }
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public TeamColor Color { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}