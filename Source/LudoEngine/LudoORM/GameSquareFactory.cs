using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.IO;


namespace LudoEngine.BoardUnits.Main
{
    public static class GameSquareFactory
    {
        public static List<IGameSquare> CreateGameSquares(string filePath)
        {
            var squares = new List<IGameSquare>();
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

        private static IGameSquare MapGameSquare(Char chr, int x, int y)
        {
            return chr switch
            {
                '0' => new SquareStandard(x, y, BoardDirection.Up),
                '1' => new SquareStandard(x, y, BoardDirection.Right),
                '2' => new SquareStandard(x, y, BoardDirection.Down),
                '3' => new SquareStandard(x, y, BoardDirection.Left),

                'a' => new SquareSafeZone(x, y, TeamColorCore.Red, BoardDirection.Down),
                'b' => new SquareSafeZone(x, y, TeamColorCore.Blue, BoardDirection.Right),
                'c' => new SquareSafeZone(x, y, TeamColorCore.Yellow, BoardDirection.Up),
                'd' => new SquareSafeZone(x, y, TeamColorCore.Green, BoardDirection.Left),

                'e' => new SquareExit(x, y, TeamColorCore.Red, BoardDirection.Right),
                'f' => new SquareExit(x, y, TeamColorCore.Blue, BoardDirection.Up),
                'g' => new SquareExit(x, y, TeamColorCore.Yellow, BoardDirection.Left),
                'h' => new SquareExit(x, y, TeamColorCore.Green, BoardDirection.Down),

                'r' => new SquareStart(x, y, TeamColorCore.Red, BoardDirection.Down),
                'l' => new SquareStart(x, y, TeamColorCore.Blue, BoardDirection.Right),
                'y' => new SquareStart(x, y, TeamColorCore.Yellow, BoardDirection.Up),
                'n' => new SquareStart(x, y, TeamColorCore.Green, BoardDirection.Left),

                '4' => new SquareTeamBase(x, y, TeamColorCore.Red, BoardDirection.Left),
                '5' => new SquareTeamBase(x, y, TeamColorCore.Blue, BoardDirection.Down),
                '6' => new SquareTeamBase(x, y, TeamColorCore.Yellow, BoardDirection.Right),
                '7' => new SquareTeamBase(x, y, TeamColorCore.Green, BoardDirection.Up),

                's' => new SquareGoal(x, y),

                _ => throw new NullReferenceException()
            };
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
    }
}
