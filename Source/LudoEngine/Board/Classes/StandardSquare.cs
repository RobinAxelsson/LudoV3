using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.Board.Classes
{
    public class StandardSquare : IGameSquare
    {
        public StandardSquare(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(Pawn pawn) => DefaultDirection;
    }
}