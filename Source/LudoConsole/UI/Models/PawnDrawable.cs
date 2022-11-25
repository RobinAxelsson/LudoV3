using System;
using LudoConsole.UI.Interfaces;

namespace LudoConsole.UI.Models
{
    public record PawnDrawable : IDrawable
    {
        public PawnDrawable((int x, int y) coords, ConsoleColor pawnColor, ConsoleColor? squareColor, char chr = ' ')
        {
            CoordinateX = coords.x;
            CoordinateY = coords.y;
            BackgroundColor = pawnColor == squareColor ? UiColor.PawnInverseColor : pawnColor;
            ForegroundColor = pawnColor == squareColor ? pawnColor : UiColor.DarkAccent;
            Chars = pawnColor == squareColor ? "x" : chr.ToString();
        }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public bool IsDrawn { get; set; }
        public bool DoErase { get; set; }

        public bool IsSame(IDrawable drawable)
        {
            if (CoordinateX != drawable.CoordinateX) return false;
            if (CoordinateY != drawable.CoordinateY) return false;
            if (Chars != drawable.Chars) return false;
            if (ForegroundColor != drawable.ForegroundColor) return false;
            if (BackgroundColor != drawable.BackgroundColor) return false;
            return true;
        }
    }
}
