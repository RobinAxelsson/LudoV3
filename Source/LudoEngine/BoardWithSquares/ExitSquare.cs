using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class ExitSquare : IGameSquare
    {
        public ExitSquare(int boardX, int boardY, TeamColor color, BoardDirection defaultDirection)
        {
            BoardX = boardX;
            BoardY = boardY;
            Color = color;
            DefaultDirection = defaultDirection;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public TeamColor? Color { get; set; }
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColor Color)
        {
            if (this.Color == Color)
            {
                return
                    Color == TeamColor.Yellow ? BoardDirection.Up :
                    Color == TeamColor.Blue ? BoardDirection.Right :
                    Color == TeamColor.Red ? BoardDirection.Down : BoardDirection.Left;
            }
            else
                return DefaultDirection;
        }
    }
}