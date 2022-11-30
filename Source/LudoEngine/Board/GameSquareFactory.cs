using System;
using System.Collections.Generic;
using System.IO;
using LudoEngine.Board.Square;
using LudoEngine.Enums;
using LudoEngine.Interfaces;

namespace LudoEngine.Board
{
    internal static class GameSquareFactory
    {
        public static List<GameSquareBase> CreateGameSquares(string filePath)
        {
            var squares = new List<GameSquareBase>();
            var charCoords = ReadCharCoords(filePath);

            foreach (var charCoord in charCoords)
            {
                var chr = charCoord.chr;
                var x = charCoord.X;
                var y = charCoord.Y;
                squares.Add(MapGameSquare(chr, x, y));
            }

            return squares;
        }

        private static List<(char chr, int X, int Y)> ReadCharCoords(string filePath)
        {

            var charCoord = new List<(char chr, int X, int Y)>();
            string[] lines = File.ReadAllLines(filePath);

            var x = 0;
            var y = 0;
            foreach (var line in lines)
            {
                if (line[0] == '/') break;
                foreach (var chr in line)
                {
                    if (chr != ' ')
                        charCoord.Add((chr, x, y));
                    x++;
                }
                y++;
                x = 0;
            }
            return charCoord;
        }

        private static GameSquareBase MapGameSquare(char chr, int x, int y)
        {
            return chr switch
            {
                '0' => new GameSquareStandard(x, y, BoardDirection.Up),
                '1' => new GameSquareStandard(x, y, BoardDirection.Right),
                '2' => new GameSquareStandard(x, y, BoardDirection.Down),
                '3' => new GameSquareStandard(x, y, BoardDirection.Left),

                'a' => new GameSquareSafeZone(x, y, TeamColor.Red, BoardDirection.Down),
                'b' => new GameSquareSafeZone(x, y, TeamColor.Blue, BoardDirection.Right),
                'c' => new GameSquareSafeZone(x, y, TeamColor.Yellow, BoardDirection.Up),
                'd' => new GameSquareSafeZone(x, y, TeamColor.Green, BoardDirection.Left),

                'e' => new GameSquareExit(x, y, TeamColor.Red, BoardDirection.Right),
                'f' => new GameSquareExit(x, y, TeamColor.Blue, BoardDirection.Up),
                'g' => new GameSquareExit(x, y, TeamColor.Yellow, BoardDirection.Left),
                'h' => new GameSquareExit(x, y, TeamColor.Green, BoardDirection.Down),

                'r' => new GameSquareStart(x, y, TeamColor.Red, BoardDirection.Down),
                'l' => new GameSquareStart(x, y, TeamColor.Blue, BoardDirection.Right),
                'y' => new GameSquareStart(x, y, TeamColor.Yellow, BoardDirection.Up),
                'n' => new GameSquareStart(x, y, TeamColor.Green, BoardDirection.Left),

                '4' => new GameSquareTeamBase(x, y, TeamColor.Red, BoardDirection.Left),
                '5' => new GameSquareTeamBase(x, y, TeamColor.Blue, BoardDirection.Down),
                '6' => new GameSquareTeamBase(x, y, TeamColor.Yellow, BoardDirection.Right),
                '7' => new GameSquareTeamBase(x, y, TeamColor.Green, BoardDirection.Up),

                's' => new GameSquareGoal(x, y),

                _ => throw new NullReferenceException()
            };
        }
    }
}
