using System;

namespace LudoConsole.View.Components.Models
{
    internal sealed class ConsolePixel
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
            return Equals((ConsolePixel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CoordinateX, CoordinateY, Chars, (int) BackgroundColor, (int) ForegroundColor);
        }

        private bool Equals(ConsolePixel other)
        {
            return CoordinateX == other.CoordinateX
                   && CoordinateY == other.CoordinateY
                   && Chars == other.Chars
                   && ForegroundColor == other.ForegroundColor
                   && BackgroundColor == other.BackgroundColor;
        }

        public static ConsolePixel Square(char chr, (int X, int Y) coord, ConsoleColor backgroundColor,
            ConsoleColor foreGroundColor = ColorManager.DefaultBoardChars)
        {
            return new ConsolePixel
            {
                CoordinateX = coord.X,
                CoordinateY = coord.Y,
                BackgroundColor = backgroundColor,
                ForegroundColor = foreGroundColor,
                Chars = chr.ToString()
            };
        }

        public static ConsolePixel Pawn((int x, int y) coords, ConsoleColor pawnColor, ConsoleColor? squareColor,
            char chr = ' ')
        {
            return new ConsolePixel
            {
                CoordinateX = coords.x,
                CoordinateY = coords.y,
                BackgroundColor = pawnColor == squareColor ? ColorManager.PawnInverseColor : pawnColor,
                ForegroundColor = pawnColor == squareColor ? pawnColor : ColorManager.DarkAccent,
                Chars = pawnColor == squareColor ? "x" : chr.ToString()
            };
        }

        public static ConsolePixel Text(int coordX, int coordY, char chr)
        {
            return new ConsolePixel
            {
                CoordinateX = coordX,
                CoordinateY = coordY,
                Chars = chr.ToString()
            };
        }
    }
}