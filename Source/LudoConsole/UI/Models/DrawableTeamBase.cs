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
            var (charPoints, pawnCoords) = LudoSquareFactory.CreateCharCoords(frameSize, filePath, square.Color);
            CharCoords = LudoSquareFactory.MapToValueTuples(charPoints);
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
    }
}
