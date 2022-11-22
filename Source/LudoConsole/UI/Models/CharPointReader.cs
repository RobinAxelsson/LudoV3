using System.Collections.Generic;
using System.IO;
using LudoConsole.Exceptions;

namespace LudoConsole.UI.Models
{
    internal static class CharPointReader
    {
        public static (IEnumerable<(char chr, (int X, int Y) coords)> defaultCharPoints, IEnumerable<(int X, int Y)> pawnCharPoint) 
            GetCharPoints(string filePath, (int X, int Y) translateUpLeft)
        {
            if (translateUpLeft.X < 0 || translateUpLeft.Y < 0)
                throw new LudoConsoleWindowOutOfRangeException("Only positive coordinates allowed");

            var drawCoords = new List<(char chr, (int X, int Y) coords)>();
            var pawnCoords = new List<(int X, int Y)>();

            var lines = File.ReadAllLines(filePath);
        
            var x = -1;
            var y = -1;

            foreach (var line in lines)
            {
                y++;
                foreach (char chr in line)
                {
                    x++;
                    if (chr == 'X')
                    {
                        pawnCoords.Add((translateUpLeft.X + x, translateUpLeft.Y + y));
                        continue;
                    }
   
                    drawCoords.Add((chr, (translateUpLeft.X + x, translateUpLeft.Y + y)));
                }
                x = -1;
            }
            return (drawCoords, pawnCoords);
        }
    }
}