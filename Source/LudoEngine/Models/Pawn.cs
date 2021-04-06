using System.Collections.Generic;
using LudoEngine.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LudoEngine.Board.Classes;
using LudoEngine.Board.Intefaces;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public Pawn(TeamColor color)
        {
            Color = color;
        }
        public int ID { get; set; }
        [ForeignKey("Game")]
        public virtual Game GameID { get; set; }
        public TeamColor Color { get; set; }
        public IGameSquare CurrentSquare { get; set; }
        public bool Based { get; set; } //Kolla om Pawn ligger i basen
        public void Move(int dice)
        {
            var gameSquares = BoardHolder.BoardSquares;
            var tempSquare = CurrentSquare;
            CurrentSquare.Pawns.Remove(this);
            for (var i = 0; i <= dice; i++)
            {
                tempSquare = BoardHolder.GetNext(gameSquares, tempSquare, Color);
            }
            foreach (var pawn in tempSquare.Pawns.Where(pawn => pawn.Color != Color))
            {
                Eradicate(pawn, tempSquare);
            }
            tempSquare.Pawns.Add(this);
        }

        private void Eradicate(Pawn pawnToEradicate, IGameSquare square)
        {
            square.Pawns.Remove(pawnToEradicate);
            //Lägg till logik för att skicka till "basen"
        }
    }
}