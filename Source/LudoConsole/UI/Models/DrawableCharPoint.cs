using System;

namespace LudoConsole.UI.Models
{
    public class DrawableCharPoint
    {
        public int CoordinateX { get; init; }
        public int CoordinateY { get; init; }
        public string Chars { get; init; }
        public ConsoleColor BackgroundColor { get; init; }
        public ConsoleColor ForegroundColor { get; init; }
        public bool IsDrawn { get; set; }
        public bool DoErase { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((DrawableCharPoint) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CoordinateX, CoordinateY, Chars, (int)BackgroundColor, (int)ForegroundColor);
        }

        private bool Equals(DrawableCharPoint other)
        {
            return CoordinateX == other.CoordinateX
                   && CoordinateY == other.CoordinateY
                   && Chars == other.Chars
                   && ForegroundColor == other.ForegroundColor
                   && BackgroundColor == other.BackgroundColor;
        }

        public static DrawableCharPoint Square(char chr, (int X, int Y) coord, ConsoleColor backgroundColor,
            ConsoleColor foreGroundColor = UiColor.DefaultBoardChars)
        {
            return new DrawableCharPoint()
            {
                CoordinateX = coord.X,
                CoordinateY = coord.Y,
                BackgroundColor = backgroundColor,
                ForegroundColor = foreGroundColor,
                Chars = chr.ToString()
            };
        }

        public static DrawableCharPoint Pawn((int x, int y) coords, ConsoleColor pawnColor, ConsoleColor? squareColor, char chr = ' ')
        {
            return new DrawableCharPoint()
            {
                CoordinateX = coords.x,
                CoordinateY = coords.y,
                BackgroundColor = pawnColor == squareColor ? UiColor.PawnInverseColor : pawnColor,
                ForegroundColor = pawnColor == squareColor ? pawnColor : UiColor.DarkAccent,
                Chars = pawnColor == squareColor ? "x" : chr.ToString()
            };
        }

        public static DrawableCharPoint Text(int coordX, int coordY, char chr)
        {
            return new DrawableCharPoint()
            {
                CoordinateX = coordX,
                CoordinateY = coordY,
                Chars = chr.ToString()
            };
        }
    }
}