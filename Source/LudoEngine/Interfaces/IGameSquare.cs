using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;

namespace LudoEngine.Interfaces
{
    public interface IGameSquare
    {
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<Pawn> Pawns { get; set; }
        public BoardDirection DefaultDirection { get; set; }
        public TeamColor? Color { get; set; }
        public BoardDirection DirectionNext(TeamColor Color);
    }
}