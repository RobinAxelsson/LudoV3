using System.Collections.Generic;
using LudoEngine.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public Pawn(TeamColor color)
        {
            Color = color;
        }
        public bool IsSelected { get; set; }
        public int ID { get; set; }
        [ForeignKey("Game")]
        public virtual Game GameID { get; set; }
        public TeamColor Color { get; set; }
        public IGameSquare CurrentSquare() => Board.BoardSquares.Find(x => x.Pawns.Contains(this));
        public bool Based { get; set; } //Kolla om Pawn ligger i basen
        public void Move(int dice)
        {
            var gameSquares = Board.BoardSquares;
            var tempSquare = CurrentSquare();
            tempSquare.Pawns.Remove(this);
            for (var i = 1; i <= dice; i++)
            {
                tempSquare = Board.GetNext(gameSquares, tempSquare, Color);
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