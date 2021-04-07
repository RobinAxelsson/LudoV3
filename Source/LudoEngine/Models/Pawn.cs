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
        private static int highestId { get; set; }
        static Pawn() => highestId = 0;
        public int Id { get; } //Game Id not SQL
        public Pawn(TeamColor color)
        { 
            Color = color;
            Id = highestId;
            highestId++;
        }
        public bool IsSelected { get; set; }
        public TeamColor Color { get; set; }
        public IGameSquare CurrentSquare() => Board.BoardSquares.Find(x => x.Pawns.Contains(this, new PawnComparer()));
        public bool Based() => Board.PawnsInBase(Color).Contains(this, new PawnComparer()); //Kolla om Pawn ligger i basen
        public void Move(int dice)
        {
            var gameSquares = Board.BoardSquares;
            var tempSquare = CurrentSquare();
            tempSquare.Pawns.Remove(this);

            for (var i = 0; i < dice; i++)
            {
                tempSquare = Board.GetNext(gameSquares, tempSquare, Color);
            }
            if(tempSquare.Pawns.Count != 0 && tempSquare is not GoalSquare && tempSquare.Pawns[0].Color != Color)
            {
                var eradicateBase = Board.BaseSquare(tempSquare.Pawns[0].Color);
                eradicateBase.Pawns.AddRange(tempSquare.Pawns);
                tempSquare.Pawns.Clear();
            }
            tempSquare.Pawns.Add(this);
            this.IsSelected = false;
        }
    }
}