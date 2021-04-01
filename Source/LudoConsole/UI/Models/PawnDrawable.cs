using System;

namespace LudoConsole.UI.Models
{
    public class PawnDrawable : IDrawable
    {
        public PawnDrawable((int x, int y) coords, ConsoleColor pawnColor, ConsoleColor squareColor, char chr = ' ')
        {
            CoordinateX = coords.x;
            CoordinateY = coords.y;
            BackgroundColor = pawnColor == squareColor ? ConsoleColor.White : pawnColor;
            ForegroundColor = pawnColor == squareColor ? pawnColor : ConsoleColor.Black;
            Chars = pawnColor == squareColor ? "x" : chr.ToString();
        }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public bool IsDrawn { get; set; }
        public bool Erase { get; set; }

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
