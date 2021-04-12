
using System;
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Creation;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Dice;
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
            //var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            //var drawGameBoard = Menu.SelectedOptions(selected);

            var writerThread = UiBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .LoopCondition(() => true)
                .ToWriterThread();

            //var builder = GameBuilder.StartBuild()
            //    .MapBoard(@"LudoORM/Map/BoardMap.txt")
            //    .AddDice(new RiggedDice(new int[] { 1, 3, 6 }))
            //    .SetControl(ConsoleDefaults.KeyboardControl)
            //    .SetInfoDisplay(ConsoleDefaults.display)
            //    .NewGame();

            //var game = builder.AddAIPlayer(TeamColor.Blue, true)
            //      .AddAIPlayer(TeamColor.Green, true)
            //      .AddHumanPlayer(TeamColor.Red)
            //      .AddHumanPlayer(TeamColor.Yellow)
            //      .SetUpPawns()
            //      .StartingColor(TeamColor.Blue)
            //      .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
            //      .ToGamePlay();

            var startPoint = new PawnSavePoint()
            {
                Color = TeamColor.Blue,
                XPosition = 4,
                YPosition = 0
            };
            var startPoint2 = new PawnSavePoint()
            {
                Color = TeamColor.Red,
                XPosition = 5,
                YPosition = 0
            };

                writerThread.Start();
                //game.Start();
            }
            else if (drawGameBoard == 1)
            {
                var loadGame = GameBuilder.StartBuild()
                    .MapBoard(@"BoardUnits/Map/BoardMap.txt")
                    .AddDice(new Dice(1, 6))
                    .SetControl(ConsoleDefaults.KeyboardControl)
                    .SetInfoDisplay(ConsoleDefaults.display)
                    .LoadPawns(null)
                    .LoadPlayers()
                    .StartingColor(TeamColor.Blue)
                    .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
                    .ToGamePlay();

                writerThread.Start();
                loadGame.Start();
            }
            else if (drawGameBoard == 2)
            {
                Console.Clear();
                Console.WriteLine("Here are all the controlls for the game.\n");
                Console.WriteLine("Use arrow keys to change beween the pawns");
                Console.WriteLine("Enter is for selecting what pawn to play");
                Console.WriteLine("Press 'X' to select two pawns when you want to move out two pawns at the time \n");
                Console.WriteLine();
            }
        }
    }
}