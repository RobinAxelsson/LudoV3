using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using System.Linq;

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
            var tempSquare = CurrentSquare();
            tempSquare.Pawns.Remove(this);

            bool lastIteration;
            bool reverse = false;
            bool landOnGoalSquare;

            for (var i = 0; i < dice; i++)
            {
                lastIteration = i == dice - 1;

                if (tempSquare is GoalSquare || reverse == true)
                {
                    tempSquare = Board.GetBack(Board.BoardSquares, tempSquare, Color);
                    reverse = true;
                }
                else
                {
                    tempSquare = Board.GetNext(Board.BoardSquares, tempSquare, Color);
                }
                if (lastIteration == true && tempSquare is GoalSquare)
                {
                    this.IsSelected = false;
                    return;
                }
            }
            if (tempSquare.Pawns.Count != 0 && tempSquare.Pawns[0].Color != Color)
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