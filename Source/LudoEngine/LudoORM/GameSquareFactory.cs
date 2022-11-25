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
                '0' => new StandardSquare(x, y, BoardDirection.Up),
                '1' => new StandardSquare(x, y, BoardDirection.Right),
                '2' => new StandardSquare(x, y, BoardDirection.Down),
                '3' => new StandardSquare(x, y, BoardDirection.Left),

                'a' => new SafezoneSquare(x, y, TeamColor.Red, BoardDirection.Down),
                'b' => new SafezoneSquare(x, y, TeamColor.Blue, BoardDirection.Right),
                'c' => new SafezoneSquare(x, y, TeamColor.Yellow, BoardDirection.Up),
                'd' => new SafezoneSquare(x, y, TeamColor.Green, BoardDirection.Left),

                'e' => new ExitSquare(x, y, TeamColor.Red, BoardDirection.Right),
                'f' => new ExitSquare(x, y, TeamColor.Blue, BoardDirection.Up),
                'g' => new ExitSquare(x, y, TeamColor.Yellow, BoardDirection.Left),
                'h' => new ExitSquare(x, y, TeamColor.Green, BoardDirection.Down),

                'r' => new StartSquare(x, y, TeamColor.Red, BoardDirection.Down),
                'l' => new StartSquare(x, y, TeamColor.Blue, BoardDirection.Right),
                'y' => new StartSquare(x, y, TeamColor.Yellow, BoardDirection.Up),
                'n' => new StartSquare(x, y, TeamColor.Green, BoardDirection.Left),

                '4' => new BaseSquare(x, y, TeamColor.Red, BoardDirection.Left),
                '5' => new BaseSquare(x, y, TeamColor.Blue, BoardDirection.Down),
                '6' => new BaseSquare(x, y, TeamColor.Yellow, BoardDirection.Right),
                '7' => new BaseSquare(x, y, TeamColor.Green, BoardDirection.Up),

                's' => new GoalSquare(x, y),

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
