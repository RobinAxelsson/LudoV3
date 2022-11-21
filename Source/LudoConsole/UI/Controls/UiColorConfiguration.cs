using LudoEngine.Enum;
using System;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Controls
{
    public static class UiColorConfiguration
    {
        internal const ConsoleColor LightAccent = ConsoleColor.Gray;
        internal const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        internal const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        internal const ConsoleColor DefaultBoardChars = ConsoleColor.Black;
        internal const ConsoleColor DropShadow = ConsoleColor.Black;
        internal const ConsoleColor PawnInverseColor = ConsoleColor.White;
        internal const ConsoleColor DarkAccent = ConsoleColor.Black;

        public static Random random = new Random();
        public static ConsoleColor RandomColor() => (ConsoleColor)random.Next(0, 15);
        public static void SetDefault()
        {
            Console.ForegroundColor = DefaultForegroundColor;
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.CursorVisible = false;
            Console.WindowWidth = 89;
            Console.WindowHeight = 38;
        }

        public static ConsoleColor TranslateColor(ConsoleTeamColor color)
        {
            return color switch
            {
                ConsoleTeamColor.Blue => ConsoleColor.DarkBlue,
                ConsoleTeamColor.Red => ConsoleColor.Red,
                ConsoleTeamColor.Yellow => ConsoleColor.Yellow,
                ConsoleTeamColor.Green => ConsoleColor.Green,
                ConsoleTeamColor.Default => LightAccent,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }

        //public static ConsoleColor TranslateColor(TeamColor color) =>
        //   color == TeamColor.Blue ? ConsoleColor.DarkBlue :
        //   color == TeamColor.Green ? ConsoleColor.Green :
        //   color == TeamColor.Red ? ConsoleColor.Red :
        //   color == TeamColor.Yellow ? ConsoleColor.Yellow : LightAccent;

    }
}
