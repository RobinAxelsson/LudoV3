using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LudoConsole.UI;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoConsole.UI.Screens;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        public static List<ISquareDrawable> DrawSquares;
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DrawSquares = UiControl.ConvertAllSquares(Board.BoardSquares);
            GameSetup.NewGame(Board.BoardSquares, players: 4);
            UiControl.SetDefault();
        }
        private static void Main(string[] args)
        {
            var writerThread = new Thread(new ThreadStart(() => {
                while (true) { 
                    ConsoleWriter.UpdateBoard(DrawSquares); 
                    Thread.Sleep(300); 
                } }));

            writerThread.Start();

            while (true)
            {
                int dieRoll = ActivePlayer.RollDice();
                var pawnsToMove = ActivePlayer.SelectablePawns(dieRoll);
                int selection = 0;

                var key = new ConsoleKeyInfo().Key;

                while (key != ConsoleKey.Enter && pawnsToMove.Count > 0)
                {
                    ActivePlayer.SelectPawn(pawnsToMove[selection]);
                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow)
                        selection++;

                    if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow)
                        selection--;

                    selection =
                        selection > pawnsToMove.Count - 1 ? 0 :
                        selection < 0 ? pawnsToMove.Count - 1 : selection;
                }
                ActivePlayer.MoveSelectedPawn(dieRoll);
                ActivePlayer.NextTeam();
            }
        }

    }
}