using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Model;
using LudoConsole.View.Components.Models;

namespace LudoConsole.View.Components
{
    internal sealed class ViewGameSquareTeam : ViewGameSquareBase
    {
        public ViewGameSquareTeam(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawn> consolePawns,
            ConsoleColor color) : base(charPoints, pawnCoords, consolePawns, color)
        {
        }

        public override List<ConsolePixel> Refresh()
        {
            if (!ConsolePawns.Any()) return CreateTeamBaseAndFramePixelsWithoutPawns();

            var squareDrawables = CreateTeamBaseAndFramePixelsWithoutPawns();
            var pawnDraws = CreatePawnConsolePixelsWithDropShadow();
            AddPawnPixelsToSquarePixels(pawnDraws, squareDrawables);

            return squareDrawables;
        }

        private List<ConsolePixel> CreateTeamBaseAndFramePixelsWithoutPawns()
        {
            var drawables = new List<ConsolePixel>();

            foreach (var charPoint in CharPoints)
            {
                var color = charPoint.Char != ' ' ? Color : ColorManager.LightAccent;
                drawables.Add(ConsolePixel.Square(charPoint.Char, (charPoint.X, charPoint.Y), color));
            }

            return drawables;
        }
    }
}