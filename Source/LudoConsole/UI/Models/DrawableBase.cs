using System;

namespace LudoConsole.UI.Models
{
    public abstract class DrawableBase
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public bool IsDrawn { get; set; }
        public bool DoErase { get; set; }
        public virtual bool IsSame(DrawableBase drawableBase)
        {
            if (CoordinateX != drawableBase.CoordinateX) return false;
            if (CoordinateY != drawableBase.CoordinateY) return false;
            if (Chars != drawableBase.Chars) return false;
            if (ForegroundColor != drawableBase.ForegroundColor) return false;
            if (BackgroundColor != drawableBase.BackgroundColor) return false;
            return true;
        }
    }
}