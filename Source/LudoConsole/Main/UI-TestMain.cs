using System;
using System.Globalization;
using LudoConsole.UI;
using LudoEngine.Board.Classes;
using LudoEngine.DbModel;

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
            ConsoleWriter.WriteXYs(Board.Positions);
            Console.ReadLine();
        }
    }
}