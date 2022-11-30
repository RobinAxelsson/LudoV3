using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LudoConsole.Enums;
using LudoConsole.Model;
using LudoConsole.View.Components;
using LudoConsole.View.Components.Models;

namespace LudoConsole.View
{
    internal static class ViewGameSquareFactory
    {
        private const string Directory = @"View/Components/AsciiArt/";
        private const string TeamBaseAsciiArt = Directory + "teambase.txt";
        private const string SquareAsciiArt = Directory + "boardsquare.txt";

        public static IEnumerable<ViewGameSquareBase> CreateUiGameSquares(IEnumerable<ConsoleGameSquare> squares)
        {
            var squareDrawables = CreateSquareDrawables(squares);
            var teamSquareDrawables = CreateTeamSquareDrawables(squares, squareDrawables);
            return squareDrawables.Concat(teamSquareDrawables).ToList();
        }

        private static IEnumerable<ViewGameSquareBase> CreateTeamSquareDrawables(IEnumerable<ConsoleGameSquare> squares,
            ViewGameSquareBase[] squareDrawables)
        {
            var (boardWidth, boardHeight) = GetBoardMaxPoint(squareDrawables);

            var teamSquareDrawables = squares
                .Where(x => x.IsBase)
                .Select(square => CreateDrawableTeamBase(boardWidth, boardHeight, square));
            return teamSquareDrawables;
        }

        private static ViewGameSquareBase[] CreateSquareDrawables(IEnumerable<ConsoleGameSquare> squares)
        {
            var squareDrawables = squares.Where(x => !x.IsBase)
                .Select(CreateDrawableSquare).ToArray();
            return squareDrawables;
        }

        private static ViewGameSquareBase CreateDrawableSquare(ConsoleGameSquare square)
        {
            (int x, int y) squarePoint = (square.BoardX, square.BoardY);
            var lines = File.ReadAllLines(SquareAsciiArt);
            var truePoint = CalculateSquareTrueUpLeft(squarePoint, lines);
            var charPoints = GetCharPoints(lines, truePoint).ToList();
            var pawnCoords = FindCharXY(charPoints, 'X').ToList();
            charPoints = ReplaceCharPoints(charPoints, 'X', ' ').ToList();
            return new ViewGameSquare(charPoints.ToList(), pawnCoords.ToList(), square.Pawns,
                ColorManager.TranslateColor(square.Color));
        }

        private static ViewGameSquareBase CreateDrawableTeamBase(int boardWidth, int boardHeight, ConsoleGameSquare square)
        {
            var lines = File.ReadAllLines(TeamBaseAsciiArt);
            var trueUpLeft = CalculateTeamBaseUpLeftPoint(boardWidth, boardHeight, lines, square.Color);
            var charPoints = GetCharPoints(lines, trueUpLeft).ToList();
            var pawnCoords = FindCharXY(charPoints, 'X');
            charPoints = ReplaceCharPoints(charPoints, 'X', ' ').ToList();
            return new ViewGameSquareTeam(charPoints.ToList(), pawnCoords.ToList(), square.Pawns,
                ColorManager.TranslateColor(square.Color));
        }

        private static (int x, int y) GetBoardMaxPoint(IEnumerable<ViewGameSquareBase> drawableSquares)
        {
            var x = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.Y);
            return (x, y);
        }

        private static (int X, int Y) CalculateTeamBaseUpLeftPoint(int boardWidth, int boardHeight,
            IReadOnlyCollection<string> lines, ConsoleTeamColor teamColor)
        {
            var xMax = lines.ToList().Select(x => x.Length).Max();
            var yMax = lines.Count;

            (int X, int Y) trueUpLeft = teamColor == ConsoleTeamColor.Red ? (boardWidth - xMax + 1, 0) :
                teamColor == ConsoleTeamColor.Blue ? (0, 0) :
                teamColor == ConsoleTeamColor.Green ? (boardWidth - xMax + 1, boardHeight - yMax + 1) :
                teamColor == ConsoleTeamColor.Yellow ? (0, boardHeight - yMax + 1) :
                throw new Exception("Base must have a team color.");
            return trueUpLeft;
        }

        private static IEnumerable<CharPoint> GetCharPoints(IEnumerable<string> lines, (int X, int Y) trueUpLeft)
        {
            var charPoints = new List<CharPoint>();

            var x = 0;
            var y = 0;

            foreach (var line in lines)
            {
                foreach (var chr in line)
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

        private static IEnumerable<(int X, int Y)> FindCharXY(IEnumerable<CharPoint> charPoints, char targetChar)
        {
            return charPoints.Where(x => x.Char == targetChar).Select(charPoint => (charPoint.X, charPoint.Y));
        }

        private static IEnumerable<CharPoint> ReplaceCharPoints(List<CharPoint> toTranslate, char targetChar,
            char replace)
        {
            var toReplace = toTranslate.Where(x => x.Char == targetChar).ToList();
            var newCharPoints = toReplace.Select(old => old with {Char = replace});
            return toTranslate.Except(toReplace).Concat(newCharPoints);
        }
    }
}