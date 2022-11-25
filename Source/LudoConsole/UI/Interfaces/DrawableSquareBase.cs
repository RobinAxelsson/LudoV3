using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.Main;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Interfaces
{
    internal abstract class DrawableSquareBase
    {
        public (int X, int Y) MaxCoord() => CharPoints.Select(x => (x.X, x.Y)).Max(x => (x.X, x.Y));
        public abstract List<IDrawable> Refresh();
        //protected ConsoleGameSquare Square { get; }
        protected ConsoleColor Color { get; }
        protected List<CharPoint> CharPoints { get; }
        protected List<(int X, int Y)> PawnCoords { get; }
        protected List<ConsolePawnDto> Pawns { get; }

        protected DrawableSquareBase(List<CharPoint> charPoints, List<(int X, int Y)> pawnCoords, List<ConsolePawnDto> Pawns, ConsoleColor color)
        {
            CharPoints = charPoints;
            PawnCoords = pawnCoords;
            this.Pawns = Pawns;
            //Square = square;
            Color = color;
        }
    }
}