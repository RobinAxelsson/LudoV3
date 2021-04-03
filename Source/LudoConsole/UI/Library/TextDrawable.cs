using LudoConsole.UI.Controls;
using System;

namespace LudoConsole.UI
{
    public class TextDrawable : IDrawable
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; } = UiControl.DefaultBackgroundColor;
        public ConsoleColor ForegroundColor { get; set; } = UiControl.DefaultForegroundColor;
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