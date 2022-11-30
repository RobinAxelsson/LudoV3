using System;
using System.Diagnostics;
using LudoEngine.Enums;

namespace LudoEngine.Board.Square
{
    internal sealed class GameSquareGoal : GameSquareBase
    {
        public GameSquareGoal(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public override BoardDirection DirectionNext(TeamColor color)
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
    }
}