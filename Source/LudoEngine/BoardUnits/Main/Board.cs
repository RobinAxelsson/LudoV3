using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LudoEngine.BoardUnits.Main
{
    public static partial class Board
    {
        public static List<IGameSquare> BoardSquares { get; set; }
        static Board()
        {
            BoardSquares = BoardOrm.Map();
        }
        private static List<IGameSquare> GetTeamSquares(TeamColor color) =>
        BoardSquares.FindAll(x => x.Pawns.Count > 0).FindAll(x => x.Pawns[0].Color == color);
        public static List<IGameSquare> PawnBoardSquares(TeamColor color) =>
            GetTeamSquares(color).FindAll(x => x.GetType() != typeof(BaseSquare) && x.GetType() != typeof(GoalSquare));
        public static List<Pawn> PawnsInBase(TeamColor color) =>
            BoardSquares.Find(x => x.GetType() == typeof(BaseSquare) && x.Color == color).Pawns;
        public static List<Pawn> PawnsInGoal(TeamColor color) =>
            BoardSquares.Find(x => x.GetType() == typeof(GoalSquare)).Pawns.FindAll(x => x.Color == color);
        public static IGameSquare StartSquare(TeamColor color)
            => BoardSquares.Find(x => x.GetType() == typeof(StartSquare) && x.Color == color);
        public static List<IGameSquare> TeamPath(TeamColor color)
        {
            var teamSquares = new List<IGameSquare>();
            var start = StartSquare(color);
            teamSquares.Add(start);
            IGameSquare temp = start;
            while (temp.GetType() != typeof(GoalSquare))
            {
                temp = GetNext(BoardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }
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
    }
}
