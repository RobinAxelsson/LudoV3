using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    public class SquareDrawable : ISquareDrawable
    {
        private const string _filepath = @"UI/Map/square.txt";
        private List<(char chr, (int X, int Y) coords)> CharCoords { get; set; }
        public ConsoleGameSquare Square { get; set; }
        public (int X, int Y) MaxCoord() 
        {
            var x = CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => x.X);
            var y = CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => x.Y);
            return (x, y);
        }

        public SquareDrawable(ConsoleGameSquare square, string filePath = _filepath)
        {
            Square = square;
            CharCoords = ReadCharCoords(filePath);
        }
        private (int X, int Y) TrueUpLeft { get; set; }
        private List<(int X, int Y)> PawnCoords { get; set; } = new ();
        public object UiControls { get; private set; }

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
                var pawnDraws = DrawPawns(Square.Pawns);
                var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
                int count = toRefresh.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
                if (count != Square.Pawns.Count*2) throw new Exception($"Removed {count} when pawns count: {pawnCount}");
                toRefresh.AddRange(pawnDraws);
            }
            return toRefresh;
        }
        private List<IDrawable> DrawPawns(List<ConsolePawnDto> pawns)
        {
            if (pawns.Count < 0 || pawns.Count > 4) throw new Exception("Pawns can only be 0-4");

            var drawPawns = new List<IDrawable>();
            var pawnColor = UiColor.TranslateColor(Square.Pawns[0].Color);
            for (int i = 0; i < pawns.Count; i++)
            { 
                PawnDrawable newPawn = null;
                if (pawns[i].IsSelected == true)
                    newPawn = new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), ThisBackgroundColor());
                else
                    newPawn = new PawnDrawable(PawnCoords[i], pawnColor, ThisBackgroundColor());
                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), ThisBackgroundColor());
                drawPawns.Add(newPawn);
                drawPawns.Add(dropShadow);
            }
            return drawPawns;
        }
        private List<(char chr, (int X, int Y) coords)> ReadCharCoords(string filePath)
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
                        var resultX = trueUpLeft.X + x;
                        var resultY = trueUpLeft.Y + y;
                        if (resultX < 0) throw new Exception("X have to be greater then 0.");
                        if (resultY < 0) throw new Exception("Y have to be greater then 0.");
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
        private ConsoleColor ThisBackgroundColor() => UiColor.TranslateColor(Square.Color);
       
    }
}
