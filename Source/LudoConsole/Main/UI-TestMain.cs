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
            var diceLine = new LineData((0, 9));
            Stephan stephan = new Stephan();
            stephan.StephanColor = TeamColor.Red;
            stephan.TakeOutSquare = Board.StartSquare(TeamColor.Red);
            while (true)
            {
                Thread.Sleep(200);
                //diceLine.Update("Rolling dice...");
                int dieRoll = ActivePlayer.RollDice();
                //diceLine.Update("You got a: " + dieRoll);
                //Console.ReadKey(true);
                var pawnsToMove = ActivePlayer.SelectablePawns(dieRoll);
                int selection = 0;

                var key = new ConsoleKeyInfo().Key;
                if(ActivePlayer.CurrentTeam() != TeamColor.Red)
                {
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
                    if (pawnsToMove.Count > 0) ActivePlayer.MoveSelectedPawn(dieRoll);
                    ActivePlayer.NextTeam();
                }
              else
                {
                    stephan.Play();
                }
            }
        }

    }
}