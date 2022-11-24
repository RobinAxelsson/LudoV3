﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    public class DrawableSquare : DrawableSquareBase
    {
        private const string _filepath = @"UI/Map/square.txt";

        private List<(char chr, (int X, int Y) coords)> CharCoords { get; }
        private List<(int X, int Y)> PawnCoords { get; } = new();
        public override (int X, int Y) MaxCoord()
        {
            var x = CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => x.X);
            var y = CharCoords.Select(x => (x.coords.X, x.coords.Y)).Max(x => x.Y);
            return (x, y);
        }

        public DrawableSquare(ConsoleGameSquare square, string filePath = _filepath) : base(square)
        {
           var (charCoords, pawnCoords) = CreateCharCoords(filePath, (square.BoardX, square.BoardY));
           
           CharCoords = charCoords;
           PawnCoords = pawnCoords;
        }
        
        public override List<IDrawable> Refresh()
        {
            if (!Square.Pawns.Any()) return CreateSquareDrawablesWithoutPawns();

            var squareDrawables = CreateSquareDrawablesWithoutPawns();
            var pawnDrawables = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(squareDrawables, pawnDrawables);

            return squareDrawables;
        }


        private List<IDrawable> CreateSquareDrawablesWithoutPawns()
        {
            var drawables = new List<IDrawable>();

            foreach (var charCoord in CharCoords)
            {
                var color = ThisBackgroundColor();
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
                PawnDrawable newPawn;

                newPawn = pawns[i].IsSelected 
                    ? new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), ThisBackgroundColor()) 
                    : new PawnDrawable(PawnCoords[i], pawnColor, ThisBackgroundColor());

                var dropShadow = new LudoDrawable('_', (PawnCoords[i].X + 1, PawnCoords[i].Y), ThisBackgroundColor());

                drawables.Add(newPawn);
                drawables.Add(dropShadow);
            }

            return drawables;
        }

        private static void AddPawnDrawablesToSquareDrawables(List<IDrawable> drawablesWithOutPawns, IEnumerable<IDrawable> pawnDrawables)
        {
            var pawnXYs = pawnDrawables.Select(x => (x.CoordinateX, x.CoordinateY));
            drawablesWithOutPawns.RemoveAll(x => pawnXYs.Contains((x.CoordinateX, x.CoordinateY)));
            drawablesWithOutPawns.AddRange(pawnDrawables);
        }

        private static (List<(char chr, (int X, int Y) coords)> charCoords, List<(int X, int Y)> pawnCoords) CreateCharCoords(string filePath, (int x, int y) squarePoint)
        {
            var charCoords = new List<(char chr, (int X, int Y) coords)>();
            var pawnCoords = new List<(int X, int Y)>();

            var lines = File.ReadAllLines(filePath);

            var trueUpLeft = CalculateSquareTrueUpLeft(squarePoint, lines);

            var x = 0;
            var y = 0;
            foreach (var line in lines)
            {
                foreach (var chr in line)
                {
                    char newChar;
                    if(chr == 'X')
                    {
                        var resultX = trueUpLeft.X + x;
                        var resultY = trueUpLeft.Y + y;
                        if (resultX < 0) throw new Exception("X have to be greater then 0.");
                        if (resultY < 0) throw new Exception("Y have to be greater then 0.");
                        pawnCoords.Add((trueUpLeft.X + x, trueUpLeft.Y + y));
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

            return (charCoords, pawnCoords);
        }

        private static (int X, int Y) CalculateSquareTrueUpLeft((int x, int y) squarePoint, string[] lines)
        {
            var xMax = lines.ToList().Select(x => x.Length).Max();
            var yMax = lines.Length;

            (int X, int Y) trueUpLeft = (xMax * squarePoint.x, yMax * squarePoint.y);
            return trueUpLeft;
        }
    }
}