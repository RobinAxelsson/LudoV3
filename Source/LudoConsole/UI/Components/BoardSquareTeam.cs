using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Components
{

    internal class BoardSquareTeam : BoardSquareBase
    {
        public BoardSquareTeam(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<DrawableBase> Refresh()
        {
            if (!Pawns.Any()) return CreateTeamBaseAndFrameDrawablesWithoutPawns();

            var squareDrawables = CreateTeamBaseAndFrameDrawablesWithoutPawns();
            var pawnDraws = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(pawnDraws, squareDrawables);

            return squareDrawables;
        }

        private List<DrawableBase> CreateTeamBaseAndFrameDrawablesWithoutPawns()
        {
            var drawables = new List<DrawableBase>();

            foreach (var charCoord in CharPoints)
            {
                var color = charCoord.Char != ' ' ? Color : UiColor.LightAccent;
                drawables.Add(new DrawableSquare(charCoord.Char, (charCoord.X, charCoord.Y), color));
            }

            return drawables;
        }
    }
}
