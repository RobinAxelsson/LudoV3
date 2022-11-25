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
    }
}
