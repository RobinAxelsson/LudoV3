using System;
using System.Collections.Generic;
using LudoEngine.Board.Square;
using System.Linq;
using LudoEngine.Enums;
using LudoEngine.GameLogic;

namespace LudoEngine.Board
{
    internal static class GameBoard
    {
        private static List<GameSquareBase> _boardSquares;
        public static List<GameSquareBase> BoardSquares => _boardSquares ?? throw new ArgumentNullException(nameof(BoardSquares));

        private const string _filePath = @"Board/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            _boardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }

        public static List<GameSquareBase> TeamPath(List<GameSquareBase> boardSquares, TeamColor color)
        {
            var teamSquares = new List<GameSquareBase>();
            var baseSquare = BaseSquare(boardSquares, color);
            teamSquares.Add(baseSquare);
            GameSquareBase temp = baseSquare;
            while (temp.GetType() != typeof(GameSquareGoal))
            {
                temp = GetNext(boardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }

        private static List<GameSquareBase> GetTeamSquares(List<GameSquareBase> boardSquares, TeamColor color) =>
            boardSquares.FindAll(x => x.Pawns.Count > 0).FindAll(x => x.Pawns[0].Color == color);

        public static List<GameSquareBase> PawnBoardSquares(List<GameSquareBase> boardSquares, TeamColor color) => GetTeamSquares(boardSquares, color).FindAll(x => x.GetType() != typeof(GameSquareTeamBase) && x.GetType() != typeof(GameSquareGoal));

        public static GameSquareBase StartSquare(List<GameSquareBase> boardSquares, TeamColor color)
            => boardSquares.Find(x => x.GetType() == typeof(GameSquareStart) && x.Color == color);

        public static GameSquareBase BaseSquare(List<GameSquareBase> boardSquares, TeamColor color)
        {
            return boardSquares.Single(x => x.GetType() == typeof(GameSquareTeamBase) && x.Color == color);
        }

        public static GameSquareBase GetNext(List<GameSquareBase> squares, GameSquareBase gameSquareBase, TeamColor color)
        {
            var diff = NextDiff(gameSquareBase.DirectionNext(color));
            var nextSquare = squares.Find(x => x.BoardX == gameSquareBase.BoardX + diff.X && x.BoardY == gameSquareBase.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }

        public static GameSquareBase GetBack(List<GameSquareBase> squares, GameSquareBase gameSquareBase, TeamColor color)
        {
            var defaultDirection = gameSquareBase.DirectionNext((TeamColor)color);
            var backDirection = ReverseDirection(defaultDirection);
            var diff = NextDiff(backDirection);
            var nextSquare = squares.Find(x => x.BoardX == gameSquareBase.BoardX + diff.X && x.BoardY == gameSquareBase.BoardY + diff.Y) ?? throw new NullReferenceException();
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

        public static List<Pawn> PawnsInBase(List<GameSquareBase> boardSquares, TeamColor color) =>
            boardSquares.FirstOrDefault(x => x.GetType() == typeof(GameSquareTeamBase) && x.Color == color)?.Pawns ?? Array.Empty<Pawn>().ToList();

        public static GameSquareBase FindPawnSquare(List<GameSquareBase> boardSquares, Pawn pawn) => boardSquares.Find(x => x.Pawns.Contains(pawn));
        public static List<Pawn> AllBaseAndPlayingPawns(List<GameSquareBase> boardSquares) => boardSquares.SelectMany(x => x.Pawns).ToList();
        public static List<Pawn> OutOfBasePawns(List<GameSquareBase> boardSquares, TeamColor color) => AllPlayingPawns(boardSquares).FindAll(x => x.Color == color);
        public static List<Pawn> AllPlayingPawns(List<GameSquareBase> boardSquares) => boardSquares.FindAll(x => x.GetType() != typeof(GameSquareTeamBase) && x.GetType() != typeof(GameSquareGoal)).SelectMany(x => x.Pawns).ToList();

        public static List<Pawn> GetTeamPawns(List<GameSquareBase> boardSquares, TeamColor color) => boardSquares.SelectMany(x => x.Pawns).Where(x => x.Color == color).ToList();
    }
}
