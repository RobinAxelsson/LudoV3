using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LudoEngine.Board.Classes
{
    public static class BoardHolder
    {
        private static (int X, int Y) greenStart { get; set; } = (13,8);
        private static (int X, int Y) blueStart { get; set; } = (1,6);
        private static (int X, int Y) redStart { get; set; } = (8,1);
        private static (int X, int Y) yellowStart { get; set; } = (6,13);
        public static List<IGameSquare> BoardSquares { get; set; }
        public static IGameSquare StartSquare(TeamColor color)
            => color == TeamColor.Blue ? BoardSquares.Find(x => x.BoardX == blueStart.X && x.BoardY == blueStart.Y) :
               color == TeamColor.Red ? BoardSquares.Find(x => x.BoardX == redStart.X && x.BoardY == redStart.Y) :
               color == TeamColor.Green ? BoardSquares.Find(x => x.BoardX == greenStart.X && x.BoardY == greenStart.Y) :
                BoardSquares.Find(x => x.BoardX == yellowStart.X && x.BoardY == yellowStart.Y);
        static BoardHolder()
        {
            var boardSquares = new List<IGameSquare>();
            boardSquares.AddRange(CreateStandardSquares((6, 9), (6, 14), BoardDirection.Up)); //Longer rows of squares created in lines
            boardSquares.AddRange(CreateStandardSquares((6, 1), (6, 6), BoardDirection.Up));

            boardSquares.AddRange(CreateStandardSquares((8, 0), (8, 5), BoardDirection.Down));
            boardSquares.AddRange(CreateStandardSquares((8, 8), (8, 13), BoardDirection.Down));

            boardSquares.AddRange(CreateStandardSquares((0, 6), (5, 6), BoardDirection.Right));
            boardSquares.AddRange(CreateStandardSquares((8, 6), (13, 6), BoardDirection.Right));


            boardSquares.AddRange(CreateStandardSquares((9, 8), (14, 8), BoardDirection.Left));
            boardSquares.AddRange(CreateStandardSquares((1, 8), (6, 8), BoardDirection.Left));

            boardSquares.Add(new StandardSquare(0, 8, BoardDirection.Up)); //Leftovers corner pieces made separate
            boardSquares.Add(new StandardSquare(6, 0, BoardDirection.Right));
            boardSquares.Add(new StandardSquare(14, 6, BoardDirection.Down));
            boardSquares.Add(new StandardSquare(8, 14, BoardDirection.Left));

            boardSquares.AddRange(CreateSafeZoneSquares((7, 1), (7, 6), BoardDirection.Down)); //Inner safe zones
            boardSquares.AddRange(CreateSafeZoneSquares((1, 7), (6, 7), BoardDirection.Right));
            boardSquares.AddRange(CreateSafeZoneSquares((8, 7), (13, 7), BoardDirection.Left));
            boardSquares.AddRange(CreateSafeZoneSquares((7, 8), (7, 13), BoardDirection.Up));

            boardSquares.Add(new GoalSquare(7, 7)); //Center square

            boardSquares.Add(new ExitSquare(0, 7, TeamColor.Blue, defaultDirection: BoardDirection.Up));
            boardSquares.Add(new ExitSquare(7, 0, TeamColor.Red, defaultDirection: BoardDirection.Right));
            boardSquares.Add(new ExitSquare(14, 7, TeamColor.Green, defaultDirection: BoardDirection.Down));
            boardSquares.Add(new ExitSquare(7, 14, TeamColor.Yellow, defaultDirection: BoardDirection.Left));

            BoardSquares = boardSquares;
        }
        public static List<IGameSquare> TeamPath(TeamColor color)
        {
            var teamSquares = new List<IGameSquare>();
            var start = StartSquare(color);
            teamSquares.Add(start);
            var temp = start;
            while (temp.GetType() != typeof(GoalSquare))
            {
                temp = GetNext(BoardSquares, temp, color);
                teamSquares.Add(temp);
            }
            return teamSquares;
        }
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
        public static IGameSquare GetNext(List<IGameSquare> squares, IGameSquare square, TeamColor color)
        {
            var diff = NextDiff(square.DirectionNext(color));
            return squares.Find(x => x.BoardX == square.BoardX + diff.X && x.BoardY == square.BoardY + diff.Y);
        }
        public static BoardDirection FlipDirection(BoardDirection direction)
            => direction == BoardDirection.Down ? BoardDirection.Up :
               direction == BoardDirection.Up ? BoardDirection.Down :
               direction == BoardDirection.Left ? BoardDirection.Right : BoardDirection.Left;
        static List<IGameSquare> CreateStandardSquares((int X, int Y) coord1, (int X, int Y) coord2, BoardDirection direction)
        {
            var XYs = Rectangle(coord1, coord2);
            var squares = new List<IGameSquare>();
            foreach (var pos in XYs)
                squares.Add(new StandardSquare(pos.X, pos.Y, direction));
            return squares;
        }
        static List<IGameSquare> CreateSafeZoneSquares((int X, int Y) coord1, (int X, int Y) coord2, BoardDirection direction)
        {
            var XYs = Rectangle(coord1, coord2);
            var squares = new List<IGameSquare>();
            foreach (var pos in XYs)
                squares.Add(new SafezoneSquare(pos.X, pos.Y, direction));
            return squares;
        }
        private static List<(int X, int Y)> Rectangle((int X, int Y) pointA, (int X, int Y) pointB)
        {
            var positions = new List<(int X, int Y)>();
            for (int x = pointA.X; x <= pointB.X; x++)
            {
                for (int y = pointA.Y; y <= pointB.Y; y++)
                {
                    positions.Add((x, y));
                }
            }
            return positions;
        }

    }
}
