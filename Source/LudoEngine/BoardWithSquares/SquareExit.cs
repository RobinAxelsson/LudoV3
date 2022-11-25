using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class SquareExit : IGameSquare
    {
        public SquareExit(int boardX, int boardY, TeamColorCore color, BoardDirection defaultDirection)
        {
            BoardX = boardX;
            BoardY = boardY;
            Color = color;
            DefaultDirection = defaultDirection;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public TeamColorCore? Color { get; set; }
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColorCore Color)
        {
            if (this.Color == Color)
            {
                return
                    Color == TeamColorCore.Yellow ? BoardDirection.Up :
                    Color == TeamColorCore.Blue ? BoardDirection.Right :
                    Color == TeamColorCore.Red ? BoardDirection.Down : BoardDirection.Left;
            }
            else
                return DefaultDirection;
        }
    }
}