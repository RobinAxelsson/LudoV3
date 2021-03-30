using LudoEngine.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public int ID { get; set; }
        [ForeignKey("Game")]
        public virtual Game GameID { get; set; }
        public TeamColor Color { get; set; }
        public int SquareIndex { get; set; }
    }
}