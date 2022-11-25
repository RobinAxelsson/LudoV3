using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;

namespace LudoConsole.UI.Models
{

    internal class DrawableTeamBase : DrawableSquareBase
    {
        public DrawableTeamBase(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<IDrawable> Refresh()
        {
            if (!Pawns.Any()) return CreateTeamBaseAndFrameDrawablesWithoutPawns();

            var squareDrawables = CreateTeamBaseAndFrameDrawablesWithoutPawns();
            var pawnDraws = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(pawnDraws, squareDrawables);

            return squareDrawables;
        }

        private List<IDrawable> CreateTeamBaseAndFrameDrawablesWithoutPawns()
        {
            var drawables = new List<IDrawable>();

            foreach (var charCoord in CharPoints)
            {
                var color = charCoord.Char != ' ' ? Color : UiColor.LightAccent;
                drawables.Add(new LudoDrawable(charCoord.Char, (charCoord.X, charCoord.Y), color));
            }

            return drawables;
        }

        private List<IDrawable> CreatePawnDrawablesWithDropShadow()
        {
            var pawns = Pawns;
            var drawables = new List<IDrawable>();
            var pawnColor = UiColor.TranslateColor(Pawns[0].Color);

            for (var i = 0; i < pawns.Count; i++)
            {
                var newPawn = pawns[i].IsSelected
                    ? new PawnDrawable(PawnCoords[i], UiColor.RandomColor(), Color)
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
            if (count != Pawns.Count * 2) throw new Exception($"Removed {count}");
            squareDrawables.AddRange(pawnDraws);
        }
    }
}
