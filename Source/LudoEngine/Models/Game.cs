using LudoEngine.Enum;
using System;
using System.Collections.Generic;
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
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
    }

    public class PlayerGame
    {
        public int PlayerId { get; set; }
        public ICollection<Player> Player { get; set; }
        public int GameId { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}