using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LudoEngine.Models
{
    public class Game
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public string CurrentTurn { get; set; }
        public int FirstPlace { get; set; }
        public int SecondPlace { get; set; }
        public int ThirdPlace { get; set; }
        public int FourthPlace { get; set; }
        public virtual Pawn pawn { get; set; }
    }
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public Game Game { get; set; }
    }

    public class PlayerGame
    { 
     //   [Key] 
     //   public int PlayerId { get; set; }
        public Player Player { get; set; }
        
  //      [Key]
  //      public int GameId { get; set; }
        public Game Game { get; set; }
     
    }
}