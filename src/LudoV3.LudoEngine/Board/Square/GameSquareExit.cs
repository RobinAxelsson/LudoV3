using System;
using LudoEngine.Enums;

namespace LudoEngine.Board.Square
{
    internal sealed class GameSquareExit : GameSquareBase
    {
        public GameSquareExit(int boardX, int boardY, TeamColor color, BoardDirection defaultDirection)
        {
            BoardX = boardX;
            BoardY = boardY;
            Color = color;
            DefaultDirection = defaultDirection;
        }
        public override BoardDirection DirectionNext(TeamColor color)
        {
            if (Color == color)
            {
                return color switch
                {
                    TeamColor.Blue => BoardDirection.Right,
                    TeamColor.Red => BoardDirection.Down,
                    TeamColor.Yellow => BoardDirection.Up,
                    TeamColor.Green => BoardDirection.Left,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
                };
            }

            return DefaultDirection;
        }
    }
}