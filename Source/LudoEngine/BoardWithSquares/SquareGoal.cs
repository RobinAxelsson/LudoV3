using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class SquareGoal : IGameSquare
    {
        public SquareGoal(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public TeamColorCore? Color { get; set; } = null;
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColorCore Color)
        {
                return
                   Color == TeamColorCore.Yellow ? BoardDirection.Up :
                   Color == TeamColorCore.Blue ? BoardDirection.Right :
                   Color == TeamColorCore.Red ? BoardDirection.Down : BoardDirection.Left;
        }
    }
}