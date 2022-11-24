using System.Collections.Generic;
using System.IO;
using LudoConsole.Exceptions;

namespace LudoConsole.UI.Models
{
    internal static class CharUnitFactory
    {
        public static (IEnumerable<CharPoint> defaultCharPoints, IEnumerable<CharPoint> pawnCharPoint) 
            GetLudoCharPoints(string filePath, (int X, int Y) translateUpLeft)
        {
            if (translateUpLeft.X < 0 || translateUpLeft.Y < 0)
                throw new LudoConsoleWindowOutOfRangeException("Only positive coordinates allowed");

            var drawCoords = new List<CharPoint>();
            var pawnCoords = new List<CharPoint>();

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
                        pawnCoords.Add(new CharPoint(' ', translateUpLeft.X + x, translateUpLeft.Y + y));
                        continue;
                    }
   
                    drawCoords.Add(new CharPoint(chr, translateUpLeft.X + x, translateUpLeft.Y + y));
                }
                x = -1;
            }
            return (drawCoords, pawnCoords);
        }
    }
}