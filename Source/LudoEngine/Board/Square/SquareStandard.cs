using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board.Square
{
    internal sealed class SquareStandard : SquareBase
    {
        public SquareStandard(int boardX, int boardY, BoardDirection direction)
        {
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }

        public override BoardDirection DirectionNext(TeamColor color) => DefaultDirection;
    }
}