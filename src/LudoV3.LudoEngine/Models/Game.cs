using System;
using LudoEngine.Enums;

namespace LudoEngine.Models
{
    internal class Game
    {
        public int Id { get; set; }
        public TeamColor CurrentTurn { get; set; }
        public int? FirstPlace { get; set; }
        public int? SecondPlace { get; set; }
        public int? ThirdPlace { get; set; }
        public int? FourthPlace { get; set; }
        public DateTime LastSaved { get; set; }
    }
}