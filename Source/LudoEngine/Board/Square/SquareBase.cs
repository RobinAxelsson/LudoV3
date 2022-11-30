using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;

namespace LudoEngine.Board.Square
{
    internal abstract class SquareBase
    {
        public int BoardX { get; init; }
        public int BoardY { get; init; }
        public List<Pawn> Pawns { get; } = new();
        public BoardDirection DefaultDirection { get; set; }
        public TeamColor? Color { get; init; } = null;
        public abstract BoardDirection DirectionNext(TeamColor color);
    }
}