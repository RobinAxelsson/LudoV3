using System;

namespace LudoConsole.UI.Models
{
    public class DrawablePawn : DrawableBase
    {
        public DrawablePawn((int x, int y) coords, ConsoleColor pawnColor, ConsoleColor? squareColor, char chr = ' ')
        {
            CoordinateX = coords.x;
            CoordinateY = coords.y;
            BackgroundColor = pawnColor == squareColor ? UiColor.PawnInverseColor : pawnColor;
            ForegroundColor = pawnColor == squareColor ? pawnColor : UiColor.DarkAccent;
            Chars = pawnColor == squareColor ? "x" : chr.ToString();
        }
    }
}
