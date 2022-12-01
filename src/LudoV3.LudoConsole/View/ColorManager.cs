using System;
using LudoConsole.Enums;

namespace LudoConsole.View
{
    internal static class ColorManager
    {
        internal const ConsoleColor LightAccent = ConsoleColor.Gray;
        internal const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        internal const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        internal const ConsoleColor DefaultBoardChars = ConsoleColor.Black;
        internal const ConsoleColor DropShadow = ConsoleColor.Black;
        internal const ConsoleColor PawnInverseColor = ConsoleColor.White;
        internal const ConsoleColor DarkAccent = ConsoleColor.Black;

        private static readonly Random _random = new();

        public static ConsoleColor RandomColor()
        {
            return (ConsoleColor) _random.Next(0, 15);
        }

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
    }
}