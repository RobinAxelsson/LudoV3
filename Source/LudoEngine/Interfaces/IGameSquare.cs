using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Interfaces
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