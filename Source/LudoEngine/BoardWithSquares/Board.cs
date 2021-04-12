using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LudoEngine.BoardUnits.Main
{
    public static class Board
    {
        public static List<IGameSquare> BoardSquares { get; set; }

        private const string _filePath = @"LudoORM/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            BoardSquares = BoardOrm.Map(filePath);
        }
        private static List<IGameSquare> GetTeamSquares(TeamColor color) =>
        BoardSquares.FindAll(x => x.Pawns.Count > 0).FindAll(x => x.Pawns[0].Color == color);
        public static List<IGameSquare> PawnBoardSquares(TeamColor color) =>
            GetTeamSquares(color).FindAll(x => x.GetType() != typeof(BaseSquare) && x.GetType() != typeof(GoalSquare));
        public static List<Pawn> PawnsInBase(TeamColor color) =>
            BoardSquares.Find(x => x.GetType() == typeof(BaseSquare) && x.Color == color).Pawns;
        public static List<Pawn> PawnsInGoal(TeamColor color) =>
            BoardSquares.Find(x => x.GetType() == typeof(GoalSquare)).Pawns.FindAll(x => x.Color == color);
        public static IGameSquare FindPawnSquare(Pawn pawn) => BoardSquares.Find(x => x.Pawns.Contains(pawn));
        public static List<Pawn> AllBaseAndPlayingPawns() => Board.BoardSquares.SelectMany(x => x.Pawns).ToList();
        public static List<Pawn> OutOfBasePawns(TeamColor color) => AllPlayingPawns().FindAll(x => x.Color == color);
        public static List<Pawn> AllPlayingPawns() => BoardSquares.FindAll(x => x.GetType() != typeof(BaseSquare) && x.GetType() != typeof(GoalSquare)).SelectMany(x => x.Pawns).ToList();
        public static bool IsMoreThenOneTeamLeft() => BoardSquares.SelectMany(x => x.Pawns).Select(x => x.Color).Distinct().ToList().Count > 1;
        public static IGameSquare StartSquare(TeamColor color)
            => BoardSquares.Find(x => x.GetType() == typeof(StartSquare) && x.Color == color);
        public static IGameSquare BaseSquare(TeamColor color)
    => BoardSquares.Find(x => x.GetType() == typeof(BaseSquare) && x.Color == color);
        public static List<IGameSquare> TeamPath(TeamColor color)
        {
            var teamSquares = new List<IGameSquare>();
            var baseSquare = BaseSquare(color);
            teamSquares.Add(baseSquare);
            IGameSquare temp = baseSquare;
            while (temp.GetType() != typeof(GoalSquare))
            {
                temp = GetNext(BoardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }
        public static List<Pawn> GetTeamPawns(TeamColor color) => BoardSquares.SelectMany(x => x.Pawns).Where(x => x.Color == color).ToList();
        private static (int X, int Y) NextDiff(BoardDirection direction) =>
                direction == BoardDirection.Up ? (0, -1) :
                direction == BoardDirection.Right ? (1, 0) :
                direction == BoardDirection.Down ? (0, 1) :
                direction == BoardDirection.Left ? (-1, 0) : (0, 0);
        public static IGameSquare GetNext(List<IGameSquare> squares, IGameSquare square, TeamColor color)
        {
            var diff = NextDiff(square.DirectionNext(color));
            var nextSquare = squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }
        public static BoardDirection ReverseDirection(BoardDirection direction) =>
            direction == BoardDirection.Down ? BoardDirection.Up :
            direction == BoardDirection.Up ? BoardDirection.Down :
            direction == BoardDirection.Left ? BoardDirection.Right : BoardDirection.Left;

        public static IGameSquare GetBack(List<IGameSquare> squares, IGameSquare square, TeamColor color)
        {
            var defaultDirection = square.DirectionNext(color);
            var backDirection = ReverseDirection(defaultDirection);
            var diff = NextDiff(backDirection);
            var nextSquare = squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y) ?? throw new NullReferenceException();
            return nextSquare;
        }
    }
}
