using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board.Square
{
    internal sealed class SquareExit : SquareBase
    {
        public SquareExit(int boardX, int boardY, TeamColor color, BoardDirection defaultDirection)
        {
            BoardX = boardX;
            BoardY = boardY;
            Color = color;
            DefaultDirection = defaultDirection;
        }
        public override BoardDirection DirectionNext(TeamColor color)
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