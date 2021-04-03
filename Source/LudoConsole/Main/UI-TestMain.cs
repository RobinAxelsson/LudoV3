using System;
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.UI;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoConsole.UI.Screens;
using LudoEngine.BoardUnits.Main;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        public static List<ISquareDrawable> DrawSquares;
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DrawSquares = UiControl.ConvertAllSquares(Board.BoardSquares);
            PawnKing.GameSetUp(Board.BoardSquares, players: 4);
            UiControl.SetDefault();
        }
        private static void Main(string[] args)
        {
            ConsoleWriter.UpdateBoard(DrawSquares);
            Console.ReadLine();
        }
    }
}