using LudoConsole.UI.Controls;
using LudoConsole.UI.Interfaces;
using System;

namespace LudoConsole.UI.Models
{
    public class LudoDrawable : IDrawable
    {
        public LudoDrawable(char chr, (int X, int Y) coord, ConsoleColor backgroundColor, ConsoleColor foreGroundColor = UiColor.DefaultBoardChars)
        {
            CoordinateX = coord.X;
            CoordinateY = coord.Y;
            BackgroundColor = backgroundColor;
            ForegroundColor = foreGroundColor;
            Chars = chr.ToString();
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
