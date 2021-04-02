using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LudoConsole.UI.Models
{
    public class BaseDrawable : ISquareDrawable
    {
        private const string _filepath = @"UI/Map/base.txt";
        private List<(char chr, (int X, int Y) coords)> CharCoords { get; set; }
        public IGameSquare Square { get; set; }
        public (int X, int Y) MaxCoord() => CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => (x.X, x.Y));
        public BaseDrawable(IGameSquare square, (int X, int Y) frameSize, string filePath = _filepath)
        {
            Square = square;
            CharCoords = ReadCharCoords(frameSize, filePath);
        }
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
            if (pawnCount > 0)
            {
                var pawnDraws = DrawPawns(pawnCount);
                var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
                int count = toRefresh.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
                if (count != Square.Pawns.Count * 2) throw new Exception($"Removed {count} when pawns count: {pawnCount}");
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
        private List<(char chr, (int X, int Y) coords)> ReadCharCoords((int X, int Y) frameSize, string filePath)
        {
            
            var charCoords = new List<(char chr, (int X, int Y) coords)>();
            string[] lines = File.ReadAllLines(filePath);

            int xMax = lines.ToList().Select(x => x.Length).Max();
            int yMax = lines.Length;

            (int X, int Y) trueUpLeft = Square.Color == TeamColor.Red ? (frameSize.X - xMax, 0) :
            Square.Color == TeamColor.Blue ? (0, 0) :
            Square.Color == TeamColor.Green ? (frameSize.X - xMax, frameSize.Y - yMax) :
            Square.Color == TeamColor.Yellow ? (0, frameSize.Y - yMax) : throw new Exception("Base must have a team color.");

            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                foreach (char chr in line)
                {
                    char newChar;
                    if (chr == 'X')
                    {
                        var resultX = trueUpLeft.X + x;
                        var resultY = trueUpLeft.Y + y;
                        if (resultX < 0) throw new Exception("X have to be greater then 0.");
                        if (resultY < 0) throw new Exception("Y have to be greater then 0.");
                        PawnCoords.Add((resultX, resultY));
                        newChar = ' ';
                    }
                    else
                        newChar = chr;

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
    }
}
