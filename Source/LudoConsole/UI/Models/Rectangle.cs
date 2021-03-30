using LudoConsole.UI.Interfaces;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoConsole.UI.Models
{
    public class Rectangle : IShape
    {
        public (int X, int Y) PointA { get; set; }
        public (int X, int Y) PointB { get; set; }
        public List<(int X, int Y)> GetShape()
        {
            var positions = new List<(int X, int Y)>();
            for (int x = PointA.X; x <= PointB.X; x++)
            {
                for (int y = PointA.Y; y <= PointB.Y; y++)
                {
                    positions.Add((x, y));
                }
            }
            return positions;
        }
    }
}
