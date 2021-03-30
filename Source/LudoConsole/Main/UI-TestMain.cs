using System;
using System.Globalization;
using LudoConsole.UI;
using LudoConsole.UI.Tools;
using LudoEngine.DbModel;

namespace LudoConsole.Main
{
    internal static class UITestMain
    {
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private static void Main33(string[] args)
        {
            ConsoleWriter.WriteXYs(BoardPositions.Positions);
            Console.ReadLine();
        }
    }
}