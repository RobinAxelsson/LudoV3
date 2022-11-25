using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class SquareStandard : IGameSquare
    {
        public SquareStandard(int boardX, int boardY, BoardDirection direction)
        {
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }

        public TeamColorCore? Color { get; set; } = null;
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColorCore Color) => DefaultDirection;
    }
}