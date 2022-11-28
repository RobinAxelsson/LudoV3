using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board.Square
{
    public class SquareStandard : IGameSquare
    {
        public SquareStandard(int boardX, int boardY, BoardDirection direction)
        {
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }

        public TeamColor? Color { get; set; } = null;
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColor Color) => DefaultDirection;
    }
}