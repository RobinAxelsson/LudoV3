using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LudoConsole.UI.Models
{

    public class DrawableTeamBase : DrawableSquareBase
    {
        private const string _filepath = @"UI/Map/base.txt";

        private List<(char chr, (int X, int Y) coords)> CharCoords { get; }
        private List<(int X, int Y)> PawnCoords { get; } = new();

        public override (int X, int Y) MaxCoord() => CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => (x.X, x.Y));

        public DrawableTeamBase(ConsoleGameSquare square, (int X, int Y) frameSize, string filePath = _filepath) : base(square)
        {
            var (charCoords, pawnCoords) = CreateCharCoords(frameSize, filePath);
            CharCoords = charCoords;
            PawnCoords = pawnCoords;
        }


        public override List<IDrawable> Refresh()
        {

            if (!Square.Pawns.Any()) return CreateTeamBaseAndFrameDrawablesWithoutPawns();

            var squareDrawables = CreateTeamBaseAndFrameDrawablesWithoutPawns();
            var pawnDraws = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(pawnDraws, squareDrawables);

            return squareDrawables;
        }

        private List<IDrawable> CreateTeamBaseAndFrameDrawablesWithoutPawns()
        {
            var drawables = new List<IDrawable>();

            foreach (var charCoord in CharCoords)
            {
                var color = charCoord.chr != ' ' ? ThisBackgroundColor() : UiColor.LightAccent;
                drawables.Add(new LudoDrawable(charCoord.chr, charCoord.coords, color));
            }

            return drawables;
        }

        private List<IDrawable> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Square.Pawns;
            var drawables = new List<IDrawable>();
            var pawnColor = UiColor.TranslateColor(Square.Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                var newPawn = pawns[i].IsSelected
                    ? new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), ThisBackgroundColor())
                    : new PawnDrawable(PawnCoords[i], pawnColor, null);

                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), UiColor.LightAccent);
               
                drawables.Add(newPawn);
                drawables.Add(dropShadow);
            }

            return drawables;
        }

        private void AddPawnDrawablesToSquareDrawables(List<IDrawable> pawnDraws, List<IDrawable> squareDrawables)
        {
            var pawnXYs = pawnDraws.Select(x => (x.CoordinateX, x.CoordinateY));
            var count = squareDrawables.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            if (count != Square.Pawns.Count * 2) throw new Exception($"Removed {count}");
            squareDrawables.AddRange(pawnDraws);
        }

        private (List<(char chr, (int X, int Y) coords)>charCoords, List<(int X, int Y)> pawnCoords) CreateCharCoords((int X, int Y) frameSize, string filePath)
        {
            
            var charCoords = new List<(char chr, (int X, int Y) coords)>();
            var pawnCoords = new List<(int X, int Y)>();
           
            var lines = File.ReadAllLines(filePath);

            var trueUpLeft = CalculateTeamBaseUpLeftPoint(frameSize, lines);

            var x = 0;
            var y = 0;
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
                        pawnCoords.Add((resultX, resultY));
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
            return (charCoords, pawnCoords);
        }

        private (int X, int Y) CalculateTeamBaseUpLeftPoint((int X, int Y) frameSize, string[] lines)
        {
            int xMax = lines.ToList().Select(x => x.Length).Max();
            int yMax = lines.Length;

            (int X, int Y) trueUpLeft = Square.Color == ConsoleTeamColor.Red ? (frameSize.X - xMax + 1, 0) :
                Square.Color == ConsoleTeamColor.Blue ? (0, 0) :
                Square.Color == ConsoleTeamColor.Green ? (frameSize.X - xMax + 1, frameSize.Y - yMax + 1) :
                Square.Color == ConsoleTeamColor.Yellow ? (0, frameSize.Y - yMax + 1) :
                throw new Exception("Base must have a team color.");
            return trueUpLeft;
        }
    }
}
