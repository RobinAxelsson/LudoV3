using System;

namespace LudoConsole.UI.Models
{
    public class DrawableSquare : DrawableBase
    {
        public DrawableSquare(char chr, (int X, int Y) coord, ConsoleColor backgroundColor, ConsoleColor foreGroundColor = UiColor.DefaultBoardChars)
        {
            CoordinateX = coord.X;
            CoordinateY = coord.Y;
            BackgroundColor = backgroundColor;
            ForegroundColor = foreGroundColor;
            Chars = chr.ToString();
        }
    }
}
