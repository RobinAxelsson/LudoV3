using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class StartSquare : IGameSquare
    {
        public StartSquare(int boardX, int boardY, TeamColor color, BoardDirection direction)
        {
            Color = color;
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }
        public TeamColor? Color { get; set; }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColor Color) => DefaultDirection;
    }
}