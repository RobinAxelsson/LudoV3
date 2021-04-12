using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;
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
                var color = charCoord.chr != ' ' ? ThisBackgroundColor() : UiControl.LightAccent;
                toRefresh.Add(new LudoDrawable(charCoord.chr, charCoord.coords, color));
            }
            int pawnCount = Square.Pawns.Count;
            if (pawnCount > 0)
            {
                var pawnDraws = DrawPawns(Square.Pawns);
                var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
                int count = toRefresh.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
                if (count != Square.Pawns.Count * 2) throw new Exception($"Removed {count} when pawns count: {pawnCount}");
                toRefresh.AddRange(pawnDraws);
            }
            return toRefresh;
        }
        private List<IDrawable> DrawPawns(List<Pawn> pawns)
        {
            if (pawns.Count < 0 || pawns.Count > 4) throw new Exception("Pawns can only be 0-4");

            var drawPawns = new List<IDrawable>();
            var pawnColor = UiControl.TranslateColor(Square.Pawns[0].Color);
            for (int i = 0; i < pawns.Count; i++)
            {
                PawnDrawable newPawn = null;
                if (pawns[i].IsSelected == true)
                    newPawn = new PawnDrawable(PawnCoords[i], UiControl.RandomColor(), ThisBackgroundColor());
                else
                    newPawn = new PawnDrawable(PawnCoords[i], pawnColor, null);
                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), UiControl.LightAccent, UiControl.DropShadow);
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

            (int X, int Y) trueUpLeft = Square.Color == TeamColor.Red ? (frameSize.X - xMax + 1, 0) :
            Square.Color == TeamColor.Blue ? (0, 0) :
            Square.Color == TeamColor.Green ? (frameSize.X - xMax + 1, frameSize.Y - yMax + 1) :
            Square.Color == TeamColor.Yellow ? (0, frameSize.Y - yMax + 1) : throw new Exception("Base must have a team color.");

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
        private ConsoleColor ThisBackgroundColor() => Square.Color != null ? UiControl.TranslateColor((TeamColor)Square.Color) : UiControl.LightAccent;
    }
}
