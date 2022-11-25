using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LudoConsole.UI.Models
{
    internal static class LudoSquareFactory
    {
        public static (List<CharPoint> charCoords, List<(int X, int Y)> pawnCoords) CreateCharCoords(string filePath, (int x, int y) squarePoint)
        {

            var lines = File.ReadAllLines(filePath);
            var truePoint = CalculateSquareTrueUpLeft(squarePoint, lines);
            var charPoints = GetCharPoints(lines, truePoint);
            var pawnCoords = FindCharXY(charPoints, 'X');
            charPoints = ReplaceCharPoints(charPoints, 'X', ' ');
            return (charPoints.ToList(), pawnCoords.ToList());
        }


        public static (List<CharPoint> charCoords, List<(int X, int Y)> pawnCoords) CreateCharCoords((int X, int Y) frameSize, string filePath, ConsoleTeamColor teamColor)
        {

            var lines = File.ReadAllLines(filePath);
            var trueUpLeft = LudoSquareFactory.CalculateTeamBaseUpLeftPoint(frameSize, lines, teamColor);
            var charPoints = LudoSquareFactory.GetCharPoints(lines, trueUpLeft);
            var pawnCoords = LudoSquareFactory.FindCharXY(charPoints, 'X');
            charPoints = LudoSquareFactory.ReplaceCharPoints(charPoints, 'X', ' ');
            return (charPoints.ToList(), pawnCoords.ToList());
        }

        public static IEnumerable<CharPoint> GetCharPoints(string[] lines, (int X, int Y) trueUpLeft)
        {
            var charPoints = new List<CharPoint>();

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

        public static (int X, int Y) CalculateSquareTrueUpLeft((int x, int y) squarePoint, string[] lines)
        {
            var xMax = lines.ToList().Select(x => x.Length).Max();
            var yMax = lines.Length;

            (int X, int Y) trueUpLeft = (xMax * squarePoint.x, yMax * squarePoint.y);
            return trueUpLeft;
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

        public static (int X, int Y) CalculateTeamBaseUpLeftPoint((int X, int Y) frameSize, string[] lines, ConsoleTeamColor teamColor)
        {
            var xMax = lines.ToList().Select(x => x.Length).Max();
            var yMax = lines.Length;

            (int X, int Y) trueUpLeft = teamColor == ConsoleTeamColor.Red ? (frameSize.X - xMax + 1, 0) :
                teamColor == ConsoleTeamColor.Blue ? (0, 0) :
                teamColor == ConsoleTeamColor.Green ? (frameSize.X - xMax + 1, frameSize.Y - yMax + 1) :
                teamColor == ConsoleTeamColor.Yellow ? (0, frameSize.Y - yMax + 1) :
                throw new Exception("Base must have a team color.");
            return trueUpLeft;
        }
    }
}