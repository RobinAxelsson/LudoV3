using System;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;

namespace LudoEngine.BoardUnits.Main
{
    public static class BoardNavigation
    {
        public static List<IGameSquare> TeamPath(List<IGameSquare> boardSquares, TeamColorCore color)
        {
            var teamSquares = new List<IGameSquare>();
            var baseSquare = BaseSquare(teamSquares, color);
            teamSquares.Add(baseSquare);
            IGameSquare temp = baseSquare;
            while (temp.GetType() != typeof(SquareGoal))
            {
                temp = GetNext(boardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }

        private static List<IGameSquare> GetTeamSquares(List<IGameSquare> boardSquares, TeamColorCore color) =>
            boardSquares.FindAll(x => x.Pawns.Count > 0).FindAll(x => x.Pawns[0].Color == color);

        public static List<IGameSquare> PawnBoardSquares(List<IGameSquare> boardSquares, TeamColorCore color) => GetTeamSquares(boardSquares, color).FindAll(x => x.GetType() != typeof(SquareTeamBase) && x.GetType() != typeof(SquareGoal));

        public static IGameSquare StartSquare(List<IGameSquare> boardSquares, TeamColorCore color)
            => boardSquares.Find(x => x.GetType() == typeof(SquareStart) && x.Color == color);

        public static IGameSquare BaseSquare(List<IGameSquare> boardSquares, TeamColorCore color) => boardSquares.Find(x => x.GetType() == typeof(SquareTeamBase) && x.Color == color);

        public static IGameSquare GetNext(List<IGameSquare> squares, IGameSquare square, TeamColorCore color)
        {
            var diff = NextDiff(square.DirectionNext(color));
            var nextSquare = squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }

        public static IGameSquare GetBack(List<IGameSquare> squares, IGameSquare square, TeamColorCore color)
        {
            var defaultDirection = square.DirectionNext(color);
            var backDirection = ReverseDirection(defaultDirection);
            var diff = NextDiff(backDirection);
            var nextSquare = squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }

        private static BoardDirection ReverseDirection(BoardDirection direction) =>
            direction == BoardDirection.Down ? BoardDirection.Up :
            direction == BoardDirection.Up ? BoardDirection.Down :
            direction == BoardDirection.Left ? BoardDirection.Right : BoardDirection.Left;

        private static (int X, int Y) NextDiff(BoardDirection direction) =>
            direction == BoardDirection.Up ? (0, -1) :
            direction == BoardDirection.Right ? (1, 0) :
            direction == BoardDirection.Down ? (0, 1) :
            direction == BoardDirection.Left ? (-1, 0) : (0, 0);
    }
}