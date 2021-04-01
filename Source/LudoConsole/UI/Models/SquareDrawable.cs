using LudoConsole.UI.Interfaces;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Models
{
    public class SquareDrawable
    {
        private const string _filepath = @"UI/Map/square.txt";
        private List<(char chr, (int X, int Y) coords)> CharCoords { get; set; }
        public IGameSquare Square { get; set; }
        public SquareDrawable(IGameSquare square)
        {
            Square = square;
            CharCoords = ReadCharCoords();
        }
        private (int X, int Y) TrueUpLeft { get; set; }
        private List<(int X, int Y)> PawnCoords { get; set; } = new List<(int X, int Y)>();
        public List<IDrawable> Refresh()
        {
            var toRefresh = new List<IDrawable>();
            for (int i = 0; i < CharCoords.Count; i++)
            {
                var charCoord = CharCoords[i];
                var color = ThisBackgroundColor();
                toRefresh.Add(new LudoDrawable(charCoord.chr, charCoord.coords, color));
            }
            int pawnCount = Square.Pawns.Count;
            if(pawnCount > 0)
            {
                var pawnDraws = DrawPawns(pawnCount);
                var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
                int count = toRefresh.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
                if (count != Square.Pawns.Count*2) throw new Exception($"Removed {count} when pawns count: {pawnCount}");
                toRefresh.AddRange(pawnDraws);
            }
            return toRefresh;
        }
        private List<IDrawable> DrawPawns(int pawnCount)
        {
            if (pawnCount < 0 || pawnCount > 4) throw new Exception("Pawns can only be 0-4");

            var drawPawns = new List<IDrawable>();
            var pawnColor = TranslateColor(Square.Pawns[0].Color);
            for (int i = 0; i < pawnCount; i++)
            {
                var newPawn = new PawnDrawable(PawnCoords[i], pawnColor, ThisBackgroundColor());
                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), ThisBackgroundColor());
                drawPawns.Add(newPawn);
                drawPawns.Add(dropShadow);
            }
            return drawPawns;
        }
        private List<(char chr, (int X, int Y) coords)> ReadCharCoords(string filePath = _filepath)
        {
            var charCoords = new List<(char chr, (int X, int Y) coords)>();
            string[] lines = File.ReadAllLines(filePath);

            int xMax = lines.ToList().Select(x => x.Length).Max();
            int yMax = lines.Length;

            (int X, int Y) trueUpLeft = (xMax * Square.BoardX, yMax * Square.BoardY);
            TrueUpLeft = trueUpLeft;

            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                foreach (char chr in line)
                {
                    char newChar;
                    if(chr == 'X')
                    {
                        PawnCoords.Add((trueUpLeft.X + x, trueUpLeft.Y + y));
                        newChar = ' ';
                    }
                    else
                    {
                        newChar = chr;
                    }
                    charCoords.Add((newChar, (trueUpLeft.X + x, trueUpLeft.Y + y)));
                    x++;
                }
                y++;
                x = 0;
            }
            return charCoords;
        }
        private ConsoleColor ThisBackgroundColor() => Square.Color != null ? TranslateColor((TeamColor)Square.Color) : ConsoleColor.White;
        private ConsoleColor TranslateColor(TeamColor color) =>
            color == TeamColor.Blue ? ConsoleColor.DarkBlue :
            color == TeamColor.Green ? ConsoleColor.Green :
            color == TeamColor.Red ? ConsoleColor.Red :
            color == TeamColor.Yellow ? ConsoleColor.Yellow : ConsoleColor.White;
        public static List<SquareDrawable> ConvertAllSquares(List<IGameSquare> squares) => squares.Select(x => new SquareDrawable(x)).ToList();
    }
}
