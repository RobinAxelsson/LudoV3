using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board.Square
{
    internal sealed class SquareGoal : IGameSquare
    {
        public SquareGoal(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public TeamColor? Color { get; set; } = null;
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColor Color)
        {
                return
                   Color == TeamColor.Yellow ? BoardDirection.Up :
                   Color == TeamColor.Blue ? BoardDirection.Right :
                   Color == TeamColor.Red ? BoardDirection.Down : BoardDirection.Left;
        }
    }
}