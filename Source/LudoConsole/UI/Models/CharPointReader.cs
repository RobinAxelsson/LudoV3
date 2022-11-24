using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LudoConsole.UI.Models
{
    internal static class CharPointReader
    {
        public static IEnumerable<CharPoint> GetCharPoints(string filePath)
        {
            var charPoints = new List<CharPoint>();
            var lines = File.ReadAllLines(filePath);

            var x = -1;
            var y = -1;

            foreach (var line in lines)
            {
                y++;
                foreach (char chr in line)
                {
                    x++;
                    charPoints.Add(new CharPoint(chr, x, y));
                }

                x = -1;
            }

            return charPoints;
        }

        public static IEnumerable<CharPoint> TransformCharPoints(IEnumerable<CharPoint> toTranslate, int x, int y)
        {
            return toTranslate.Select(old => new CharPoint(old.Char, old.X + x, old.Y + y));
        }

        public static IEnumerable<(int X, int Y)> FindCharCoords(IEnumerable<CharPoint> charPoints, char targetChar)
        {
            return charPoints.Where(x => x.Char == targetChar).Select(charPoint => (charPoint.X, charPoint.Y));
        }

        public static IEnumerable<CharPoint> ReplaceCharPoints(IList<CharPoint> toTranslate, char targetChar, char replace)
        {
            var toReplace = toTranslate.Where(x => x.Char == targetChar);
            var newCharPoints = toReplace.Select(old => old with {Char = replace});
            return toTranslate.Except(toReplace).Concat(newCharPoints);
        }
    }
}