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
            var writerThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    ConsoleWriter.UpdateBoard(DrawSquares);
                    Thread.Sleep(300);
                }
            }));

            writerThread.Start();
            var diceLine = new LineData(0, 9);
            bool humanPlayer = true;

            while (true)
            {
                diceLine.Update($"{ActivePlayer.CurrentTeam()} rolling dice...");
                Thread.Sleep(500);
                int dieRoll = 6;//ActivePlayer.RollDice();
                diceLine.Update($"{ActivePlayer.CurrentTeam()} got {dieRoll}");
                Console.ReadKey(true);
                var pawnsToMove = ActivePlayer.SelectablePawns(dieRoll);
                bool movePawn = false;

                int selection = 0;

                var key = new ConsoleKeyInfo().Key;

                if (humanPlayer)
                {
                    while (true)
                    {
                        if(pawnsToMove.Count == 0)
                        {
                            ActivePlayer.NextTeam();
                            break;
                        }
                        if (dieRoll == 6 && Board.PawnsInBase(ActivePlayer.CurrentTeam()).Count > 1)
                        {
                            diceLine.Update("'x' for two");
                            if (key == ConsoleKey.X)
                            {
                                var basePawns = Board.BaseSquare(ActivePlayer.CurrentTeam()).Pawns;
                                for (int i = 0; i < 2; i++) basePawns[i].Move(1);
                                ActivePlayer.NextTeam();
                                break;
                            }

                        }
                        ActivePlayer.SelectPawn(pawnsToMove[selection]);
                        key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow)
                            selection++;

                        if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow)
                            selection--;

                        selection =
                            selection > pawnsToMove.Count - 1 ? 0 :
                            selection < 0 ? pawnsToMove.Count - 1 : selection;

                        if (key == ConsoleKey.Enter)
                        {
                            ActivePlayer.MoveSelectedPawn(dieRoll);
                            if (dieRoll != 6) ActivePlayer.NextTeam();
                            break;
                        }
                    }
                }
            }
        }

    }
}