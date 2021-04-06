using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Controls
{
    public static class UiControl
    {
        public const ConsoleColor LightAccent = ConsoleColor.Gray;
        public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        public const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        public const ConsoleColor DefaultBoardChars = ConsoleColor.Black;
        public const ConsoleColor DropShadow = ConsoleColor.Black;
        public const ConsoleColor PawnInverseColor = ConsoleColor.White;
        public const ConsoleColor DarkAccent = ConsoleColor.Black;

        public static void SetDefault()
        {
            Console.ForegroundColor = DefaultForegroundColor;
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.CursorVisible = false;
            Console.WindowWidth = 89;
            Console.WindowHeight = 38;
        }
        public static ConsoleColor TranslateColor(TeamColor color) =>
           color == TeamColor.Blue ? ConsoleColor.DarkBlue :
           color == TeamColor.Green ? ConsoleColor.Green :
           color == TeamColor.Red ? ConsoleColor.Red :
           color == TeamColor.Yellow ? ConsoleColor.Yellow : LightAccent;

        public static List<ISquareDrawable> ConvertAllSquares(List<IGameSquare> squares)
        {
            var squareDraws = squares.Where(x => x.GetType() != typeof(BaseSquare)).Select(x => new SquareDrawable(x));
            var x = squareDraws.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = squareDraws.Select(x => x.MaxCoord()).Max(x => x.Y);
            var baseDraws = squares.Where(x => x.GetType() == typeof(BaseSquare)).Select(square => new BaseDrawable(square, (x, y))).Select(x => (ISquareDrawable)x);
            return squareDraws.Concat(baseDraws).ToList();
        }

    }
}
