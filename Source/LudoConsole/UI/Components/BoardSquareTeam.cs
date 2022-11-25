using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Mapping;
using LudoConsole.Ui.Models;

namespace LudoConsole.Ui.Components
{
    internal class BoardSquareTeam : BoardSquareBase
    {
        public BoardSquareTeam(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns,
            ConsoleColor color) : base(charPoints, pawnCoords, Pawns, color)
        {
        }

        public override List<DrawableCharPoint> Refresh()
        {
            if (!Pawns.Any()) return CreateTeamBaseAndFrameDrawablesWithoutPawns();

            var squareDrawables = CreateTeamBaseAndFrameDrawablesWithoutPawns();
            var pawnDraws = CreatePawnDrawablesWithDropShadow();
            AddPawnDrawablesToSquareDrawables(pawnDraws, squareDrawables);

            return squareDrawables;
        }

        private List<DrawableCharPoint> CreateTeamBaseAndFrameDrawablesWithoutPawns()
        {
            var drawables = new List<DrawableCharPoint>();

            foreach (var charCoord in CharPoints)
            {
                var color = charCoord.Char != ' ' ? Color : UiColor.LightAccent;
                drawables.Add(DrawableCharPoint.Square(charCoord.Char, (charCoord.X, charCoord.Y), color));
            }

            return drawables;
        }
    }
}