using System;
using System.Globalization;
using System.Linq;
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

        private static void Main(string[] args)
        {
            Console.WindowWidth = 89;
            Console.WindowHeight = 38;
            Console.CursorVisible = false;

            var squares = Board.BoardSquares;
            var drawSquares = SquareDrawable.ConvertAllSquares(squares);
            ConsoleWriter.TryAppend(drawSquares);
            ConsoleWriter.Update();
            Console.ReadLine();
            var pawn = new Pawn();
            pawn.Color = TeamColor.Blue;
            squares[0].Pawns.Add(pawn);
            ConsoleWriter.UpdateDrawSquares(drawSquares);
            ConsoleWriter.Update();
            Console.ReadLine();
        }
    }
}