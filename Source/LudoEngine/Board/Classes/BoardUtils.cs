using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.Board.Classes
{

    public static class BoardUtils
    {
        public static (int X, int Y) NextDiff(BoardDirection direction)
        {
            switch (direction)
            {
                case BoardDirection.Up:
                    return (0, -1);
                case BoardDirection.Right:
                    return (1, 0);
                case BoardDirection.Down:
                    return (0, 1);
                case BoardDirection.Left:
                    return (-1, 0);
                default:
                    return (0, 0);
            }
        }
        public static IGameSquare GetNext(List<IGameSquare> squares, IGameSquare square, Pawn pawn)
        {
            var diff = NextDiff(square.DirectionNext(pawn));
            return squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y);
        }
        public static BoardDirection FlipDirection(BoardDirection direction)
            => direction == BoardDirection.Down ? BoardDirection.Up :
               direction == BoardDirection.Up ? BoardDirection.Down :
               direction == BoardDirection.Left ? BoardDirection.Right : BoardDirection.Left;

    }
}
