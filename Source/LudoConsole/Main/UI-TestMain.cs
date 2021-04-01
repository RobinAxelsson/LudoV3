using System;
using System.Globalization;
using System.Linq;
using LudoConsole.UI;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;

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
            var squares = Board.BoardSquares;
            var red = Board.TeamPath(TeamColor.Red);
            var green = Board.TeamPath(TeamColor.Green);
            var blue = Board.TeamPath(TeamColor.Blue);
            var yellow = Board.TeamPath(TeamColor.Yellow);


            var xys = blue.Select(x => (x.BoardX, x.BoardY)).ToList();
            ConsoleWriter.WriteXYs(xys, ConsoleColor.Blue);
            Console.ReadLine();
        }
    }
}