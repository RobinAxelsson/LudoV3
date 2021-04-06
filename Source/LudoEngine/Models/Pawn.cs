using LudoEngine.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public int Id { get; set; }
        public virtual Game Game  { get; set; }
        public TeamColor Color { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}