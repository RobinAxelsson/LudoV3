using LudoConsole.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Tools
{
    public static class BoardPositions
    {
        public static List<(int X, int Y)> Positions = new List<(int X, int Y)>();
        private static readonly (int X, int Y) UpLeft = (0, 0);
        private static readonly (int X, int Y) DownRight = (14, 14);
        static BoardPositions()
        {
            Positions = Rectangle(UpLeft, DownRight);
            Remove(Rectangle(UpLeft, (5, 5))); //upleft
            Remove(Rectangle((9, 0), (14, 5))); //upright
            Remove(Rectangle((9, 9), (DownRight))); //downright
            Remove(Rectangle((0, 9), (5, 14))); //downleft
            Remove(Rectangle((6, 6), (8, 8))); //center
        }
        private static void Remove(List<(int X, int Y)> positions)
        {
            Positions = Positions.Except(positions).ToList();
        }
        private static List<(int X, int Y)>Rectangle((int X, int Y) pointA, (int X, int Y) pointB)
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
