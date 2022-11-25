using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    internal static class LudoSquareFactory
    {
        private const string BaseAsciiArt = @"UI/Map/base.txt";
        private const string SquareAsciiArt = @"UI/Map/square.txt";

        public static IEnumerable<DrawableSquareBase> CreateBoardSquares(IEnumerable<ConsoleGameSquare> squares)
        {
            var squareDraws = squares.Where(x => !x.IsBase)
                .Select(CreateSquare).ToArray();

            var (boardWidth, boardHeight) = GetBoardMaxPoint(squareDraws);

            var baseDraws = squares
                .Where(x => x.IsBase)
                .Select(square => CreateTeamBase(boardWidth, boardHeight, square));

            return squareDraws.Concat(baseDraws).ToList();
        }

        private static (int x, int y) GetBoardMaxPoint(DrawableSquareBase[] drawableSquares)
        {
            var x = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.Y);
            return (x, y);
        }

        private static DrawableSquareBase CreateTeamBase(int boardWidth, int boardHeight, ConsoleGameSquare square)
        {
            var (charPoints, drawPoints) = CreateTeamBaseCharPoints(boardWidth, boardHeight, square.Color);
            return new DrawableSquare(charPoints, drawPoints, square);
        }

        private static DrawableSquareBase CreateSquare(ConsoleGameSquare square)
        {
            var (charPoints, drawPoints) = CreateSquareCharPoints((square.BoardX, square.BoardY));
            return new DrawableSquare(charPoints, drawPoints, square);
        }

        private static (List<CharPoint> charCoords, List<(int X, int Y)> pawnCoords) CreateSquareCharPoints((int x, int y) squarePoint)
        {

            var lines = File.ReadAllLines(SquareAsciiArt);
            var truePoint = CalculateSquareTrueUpLeft(squarePoint, lines);
            var charPoints = GetCharPoints(lines, truePoint);
            var pawnCoords = FindCharXY(charPoints, 'X');
            charPoints = ReplaceCharPoints(charPoints, 'X', ' ');
            return (charPoints.ToList(), pawnCoords.ToList());
        }


        private static (List<CharPoint> charCoords, List<(int X, int Y)> pawnCoords) CreateTeamBaseCharPoints(int boardWidth, int boardHeight, ConsoleTeamColor teamColor)
        {

            var lines = File.ReadAllLines(BaseAsciiArt);
            var trueUpLeft = CalculateTeamBaseUpLeftPoint(boardWidth, boardHeight, lines, teamColor);
            var charPoints = GetCharPoints(lines, trueUpLeft);
            var pawnCoords = FindCharXY(charPoints, 'X');
            charPoints = ReplaceCharPoints(charPoints, 'X', ' ');
            return (charPoints.ToList(), pawnCoords.ToList());
        }

        private static IEnumerable<CharPoint> GetCharPoints(string[] lines, (int X, int Y) trueUpLeft)
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

        private static (int X, int Y) CalculateSquareTrueUpLeft((int x, int y) squarePoint, string[] lines)
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

        private static IEnumerable<(int X, int Y)> FindCharXY(IEnumerable<CharPoint> charPoints, char targetChar)
        {
            return charPoints.Where(x => x.Char == targetChar).Select(charPoint => (charPoint.X, charPoint.Y));
        }

        private static IEnumerable<CharPoint> ReplaceCharPoints(IEnumerable<CharPoint> toTranslate, char targetChar, char replace)
        {
            var toReplace = toTranslate.Where(x => x.Char == targetChar);
            var newCharPoints = toReplace.Select(old => old with {Char = replace});
            return toTranslate.Except(toReplace).Concat(newCharPoints);
        }

        private static (int X, int Y) CalculateTeamBaseUpLeftPoint(int boardWidth, int boardHeight, IReadOnlyCollection<string> lines, ConsoleTeamColor teamColor)
        {
            var xMax = lines.ToList().Select(x => x.Length).Max();
            var yMax = lines.Count;

            (int X, int Y) trueUpLeft = teamColor == ConsoleTeamColor.Red ? (boardWidth - xMax + 1, 0) :
                teamColor == ConsoleTeamColor.Blue ? (0, 0) :
                teamColor == ConsoleTeamColor.Green ? (boardWidth - xMax + 1, boardHeight- yMax + 1) :
                teamColor == ConsoleTeamColor.Yellow ? (0, boardHeight - yMax + 1) :
                throw new Exception("Base must have a team color.");
            return trueUpLeft;
        }
    }
}