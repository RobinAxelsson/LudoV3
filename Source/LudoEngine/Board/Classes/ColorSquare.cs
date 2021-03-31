using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.Board.Classes
{
    public class ColorSquare : IGameSquare
    {
        public ColorSquare((int X, int Y) boardCoord, TeamColor color)
        {
            BoardX = boardCoord.X;
            BoardY = boardCoord.Y;
            Color = color;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        private TeamColor Color { get; set; }
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(Pawn pawn)
        {
            if (Color == pawn.Color)
            {
                return
                    pawn.Color == TeamColor.Yellow ? BoardDirection.Up :
                    pawn.Color == TeamColor.Blue ? BoardDirection.Right :
                    pawn.Color == TeamColor.Red ? BoardDirection.Down : BoardDirection.Left;
            }
            else
                return DefaultDirection;
        }
    }
}