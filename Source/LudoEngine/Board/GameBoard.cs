using System;
using System.Collections.Generic;
using LudoEngine.Board.Square;
using LudoEngine.Enum;
using System.Linq;
using LudoEngine.GameLogic;

namespace LudoEngine.Board
{
    internal static class GameBoard
    {
        private static List<SquareBase> _boardSquares;
        public static List<SquareBase> BoardSquares => _boardSquares ?? throw new ArgumentNullException(nameof(BoardSquares));

        private const string _filePath = @"Board/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            _boardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }

        public static List<SquareBase> TeamPath(List<SquareBase> boardSquares, TeamColor color)
        {
            var teamSquares = new List<SquareBase>();
            var baseSquare = BaseSquare(boardSquares, color);
            teamSquares.Add(baseSquare);
            SquareBase temp = baseSquare;
            while (temp.GetType() != typeof(SquareGoal))
            {
                temp = GetNext(boardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }

        private static List<SquareBase> GetTeamSquares(List<SquareBase> boardSquares, TeamColor color) =>
            boardSquares.FindAll(x => x.Pawns.Count > 0).FindAll(x => x.Pawns[0].Color == color);

        public static List<SquareBase> PawnBoardSquares(List<SquareBase> boardSquares, TeamColor color) => GetTeamSquares(boardSquares, color).FindAll(x => x.GetType() != typeof(SquareTeamBase) && x.GetType() != typeof(SquareGoal));

        public static SquareBase StartSquare(List<SquareBase> boardSquares, TeamColor color)
            => boardSquares.Find(x => x.GetType() == typeof(SquareStart) && x.Color == color);

        public static SquareBase BaseSquare(List<SquareBase> boardSquares, TeamColor color)
        {
            return boardSquares.Single(x => x.GetType() == typeof(SquareTeamBase) && x.Color == color);
        }

        public static SquareBase GetNext(List<SquareBase> squares, SquareBase squareBase, TeamColor color)
        {
            var diff = NextDiff(squareBase.DirectionNext(color));
            var nextSquare = squares.Find(x => x.BoardX == squareBase.BoardX + diff.X && x.BoardY == squareBase.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }

        public static SquareBase GetBack(List<SquareBase> squares, SquareBase squareBase, TeamColor color)
        {
            var defaultDirection = squareBase.DirectionNext(color);
            var backDirection = ReverseDirection(defaultDirection);
            var diff = NextDiff(backDirection);
            var nextSquare = squares.Find(x => x.BoardX == squareBase.BoardX + diff.X && x.BoardY == squareBase.BoardY + diff.Y) ?? throw new NullReferenceException();
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

        public static List<Pawn> PawnsInBase(List<SquareBase> boardSquares, TeamColor color) =>
            boardSquares.FirstOrDefault(x => x.GetType() == typeof(SquareTeamBase) && x.Color == color)?.Pawns ?? Array.Empty<Pawn>().ToList();

        public static SquareBase FindPawnSquare(List<SquareBase> boardSquares, Pawn pawn) => boardSquares.Find(x => x.Pawns.Contains(pawn));
        public static List<Pawn> AllBaseAndPlayingPawns(List<SquareBase> boardSquares) => boardSquares.SelectMany(x => x.Pawns).ToList();
        public static List<Pawn> OutOfBasePawns(List<SquareBase> boardSquares, TeamColor color) => AllPlayingPawns(boardSquares).FindAll(x => x.Color == color);
        public static List<Pawn> AllPlayingPawns(List<SquareBase> boardSquares) => boardSquares.FindAll(x => x.GetType() != typeof(SquareTeamBase) && x.GetType() != typeof(SquareGoal)).SelectMany(x => x.Pawns).ToList();

        public static List<Pawn> GetTeamPawns(List<SquareBase> boardSquares, TeamColor color) => boardSquares.SelectMany(x => x.Pawns).Where(x => x.Color == color).ToList();
    }
}
