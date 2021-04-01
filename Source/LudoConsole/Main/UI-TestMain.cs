using System;
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.UI;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }
        public static List<SquareDrawable> DrawSquares = SquareDrawable.ConvertAllSquares(Board.BoardSquares);
        private static void Main(string[] args)
        {
            Console.WindowWidth = 89;
            Console.WindowHeight = 38;
            Console.CursorVisible = false;

            var pawn0 = new Pawn();
            pawn0.Color = TeamColor.Blue;
            DrawSquares[0].Square.Pawns.Add(pawn0);
            ConsoleWriter.Update(DrawSquares);
            Console.ReadLine();
        }
    }
}