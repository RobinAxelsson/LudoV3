using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LudoConsole.UI.Models
{
    internal static class CharPointReader
    {
        public static IEnumerable<CharPoint> GetCharPoints(string filePath, (int X, int Y) trueUpLeft)
        {
            var charPoints = new List<CharPoint>();
            var lines = File.ReadAllLines(filePath);

            var x = 0;
            var y = 0;

            foreach (var line in lines)
            {
               
                foreach (char chr in line)
                {
                    charPoints.Add(new CharPoint(chr, trueUpLeft.X + x, trueUpLeft.Y + y));
                    x++;
                }
                y++;
                x = 0;
            }

            return charPoints;
        }

        public static IEnumerable<CharPoint> GetCharPoints(string filePath)
        {
            (int X, int Y) trueUpLeft = (0, 0);

            var charPoints = new List<CharPoint>();
            var lines = File.ReadAllLines(filePath);

            var x = 0;
            var y = 0;

            foreach (var line in lines)
            {

                foreach (char chr in line)
                {
                    charPoints.Add(new CharPoint(chr, trueUpLeft.X + x, trueUpLeft.Y + y));
                    x++;
                }
                y++;
                x = 0;
            }

            return charPoints;
        }

        public static IEnumerable<CharPoint> TransformCharPoints(IEnumerable<CharPoint> toTranslate, (int x, int y) point)
        {
            return toTranslate.Select(old => new CharPoint(old.Char, old.X + point.x, old.Y + point.y));
        }

        public static IEnumerable<(int X, int Y)> FindCharXY(IEnumerable<CharPoint> charPoints, char targetChar)
        {
            return charPoints.Where(x => x.Char == targetChar).Select(charPoint => (charPoint.X, charPoint.Y));
        }

        public static IEnumerable<CharPoint> ReplaceCharPoints(IEnumerable<CharPoint> toTranslate, char targetChar, char replace)
        {
            var toReplace = toTranslate.Where(x => x.Char == targetChar);
            var newCharPoints = toReplace.Select(old => old with {Char = replace});
            return toTranslate.Except(toReplace).Concat(newCharPoints);
        }

        //public static (int width, int height) GetCharPointHeightWidth(IEnumerable<CharPoint> toMeasure)
        //{
        //    toMeasure = toMeasure.ToArray();

        //    var xMax = toMeasure.Select(x => x.X).Max();
        //    var xMin = toMeasure.Select(x => x.X).Min();
        //    var yMax = toMeasure.Select(y => y.Y).Max();
        //    var yMin = toMeasure.Select(y => y.Y).Min();

        //    return (yMax - yMin, xMax - xMin);
        //}

        public static (int width, int height) GetCharPointHeightWidth(IEnumerable<CharPoint> toMeasure)
        {
            toMeasure = toMeasure.ToArray();

            var width = toMeasure.Select(x => x.X).Distinct().Count();
            var height = toMeasure.Select(x => x.Y).Distinct().Count();

            return (width, height);
        }

        public static List<(char chr, (int X, int Y) coords)> MapToValueTuples(IEnumerable<CharPoint> charPoints)
        {
            return charPoints.Select(charPoint => (charPoint.Char, (charPoint.X, charPoint.Y))).ToList();
        }
    }
}