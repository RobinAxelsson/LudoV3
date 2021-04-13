using LudoEngine.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LudoEngine.Models
{
    public class Game
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