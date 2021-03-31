using System;
using System.Globalization;
using System.Linq;
using LudoConsole.UI;
using LudoEngine.Board.Classes;
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
            var red = BoardHolder.TeamPath(TeamColor.Red);
            var green = BoardHolder.TeamPath(TeamColor.Green);
            var blue = BoardHolder.TeamPath(TeamColor.Blue);
            var yellow = BoardHolder.TeamPath(TeamColor.Yellow);

            var xys = green.Select(x => (x.BoardX, x.BoardY)).ToList();
            ConsoleWriter.WriteXYs(xys, ConsoleColor.Green);
            Console.ReadLine();
        }
    }
}