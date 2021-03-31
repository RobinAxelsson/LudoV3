using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using System.Collections.Generic;
using System.Linq;


namespace LudoEngine.Board.Classes
{
    public static class BoardBuilder
    {
        public static List<(int X, int Y)> Positions = new List<(int X, int Y)>();
        static BoardBuilder()
        {
            var boardSquares = new List<IGameSquare>();
            boardSquares.AddRange(CreateSquares((6,9), (6,14), BoardDirection.Up));
            boardSquares.AddRange(CreateSquares((6,6), (6,1), BoardDirection.Up));
            boardSquares.AddRange(CreateSquares((0,6), (5,6), BoardDirection.Right));
            boardSquares.AddRange(CreateSquares((8,6), (13,6), BoardDirection.Right));
            boardSquares.AddRange(CreateSquares((8,6), (13,6), BoardDirection.Down));
            boardSquares.AddRange(CreateSquares((8,0), (8,5), BoardDirection.Down));
            boardSquares.AddRange(CreateSquares((9,8), (14,8), BoardDirection.Left));
            boardSquares.AddRange(CreateSquares((1,8), (6,8), BoardDirection.Left));
            
            boardSquares.Add(new GoalSquare(7, 7));
            //boardSquares.Add(new GateSquare());
            //boardSquares.Add(new ColorSquare((7, 0));
            //boardSquares.Add(new ColorSquare((7, 7));
            //boardSquares.Add(new ColorSquare((7, 7));
        }
        static List<IGameSquare> CreateSquares((int X, int Y) coord1, (int X, int Y) coord2, BoardDirection direction)
        {
            var XYs = Rectangle(coord1, coord2);
            var squares = new List<IGameSquare>();
            foreach (var pos in XYs)
                squares.Add(new StandardSquare(pos.X, pos.Y, direction));
            return squares;
        }
        static List<IGameSquare> CreateStandardSquares((int X, int Y) coord1, (int X, int Y)coord2, BoardDirection direction)
        {
            var XYs = Rectangle(coord1, coord2);
            var squares = new List<IGameSquare>();
            foreach (var pos in XYs)
                squares.Add(new StandardSquare(pos.X, pos.Y, direction));
            return squares;
        }

        private static void Remove(List<(int X, int Y)> positions)
        {
            Positions = Positions.Except(positions).ToList();
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
