using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.IO;


namespace LudoEngine.BoardUnits.Main
{
    public static class BoardOrm
    {
        public static List<IGameSquare> Map(string filePath)
        {
            var squares = new List<IGameSquare>();
            var charCoords = ReadCharCoords(filePath);

            foreach (var charCoord in charCoords)
            {
                var chr = charCoord.chr;
                int x = charCoord.X;
                int y = charCoord.Y;

                IGameSquare newSquare =
                    chr == '0' ? new StandardSquare(x, y, BoardDirection.Up) :
                    chr == '1' ? new StandardSquare(x, y, BoardDirection.Right) :
                    chr == '2' ? new StandardSquare(x, y, BoardDirection.Down) :
                    chr == '3' ? new StandardSquare(x, y, BoardDirection.Left) :

                    chr == 'a' ? new SafezoneSquare(x, y, TeamColor.Red, BoardDirection.Down) :
                    chr == 'b' ? new SafezoneSquare(x, y, TeamColor.Blue, BoardDirection.Right) :
                    chr == 'c' ? new SafezoneSquare(x, y, TeamColor.Yellow, BoardDirection.Up) :
                    chr == 'd' ? new SafezoneSquare(x, y, TeamColor.Green, BoardDirection.Left) :

                    chr == 'e' ? new ExitSquare(x, y, TeamColor.Red, BoardDirection.Right) :
                    chr == 'f' ? new ExitSquare(x, y, TeamColor.Blue, BoardDirection.Up) :
                    chr == 'g' ? new ExitSquare(x, y, TeamColor.Yellow, BoardDirection.Left) :
                    chr == 'h' ? new ExitSquare(x, y, TeamColor.Green, BoardDirection.Down) :

                    chr == 'r' ? new StartSquare(x, y, TeamColor.Red, BoardDirection.Down) :
                    chr == 'l' ? new StartSquare(x, y, TeamColor.Blue, BoardDirection.Right) :
                    chr == 'y' ? new StartSquare(x, y, TeamColor.Yellow, BoardDirection.Up) :
                    chr == 'n' ? new StartSquare(x, y, TeamColor.Green, BoardDirection.Left) :

                    chr == '4' ? new BaseSquare(x, y, TeamColor.Red, BoardDirection.Left) :
                    chr == '5' ? new BaseSquare(x, y, TeamColor.Blue, BoardDirection.Down) :
                    chr == '6' ? new BaseSquare(x, y, TeamColor.Yellow, BoardDirection.Right) :
                    chr == '7' ? new BaseSquare(x, y, TeamColor.Green, BoardDirection.Up) :

                    chr == 's' ? new GoalSquare(x, y) : throw new NullReferenceException();

                squares.Add(newSquare);
            }

            return squares;
        }
        private static List<(char chr, int X, int Y)> ReadCharCoords(string filePath)
        {

            var charCoord = new List<(char chr, int X, int Y)>();
            string[] lines = File.ReadAllLines(filePath);

            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                if (line[0] == '/') break;
                foreach (char chr in line)
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
