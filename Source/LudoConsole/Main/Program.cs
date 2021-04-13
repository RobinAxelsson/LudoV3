using System;
using System.Globalization;
using LudoEngine.GameLogic;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Creation;
using LudoEngine.GameLogic.Dice;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Main;

namespace LudoConsole.Main
{
    internal static class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private static void Main(string[] args)
        {
            var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            var drawGameBoard = Menu.SelectedOptions(selected);

            if (drawGameBoard == 0)
            {

                var builder = GameBuilder.StartBuild()
                    .MapBoard(@"LudoORM/Map/BoardMap.txt")
                    .AddDice(new RiggedDice(new int[] { 6 }))
                    .SetInfoDisplay(ConsoleDefaults.display)
                    .NewGame();

                var humanColors = Menu.humanColor;
                var aiColors = Menu.aiColor;

                humanColors.ForEach(x => builder.AddHumanPlayer(x, ConsoleDefaults.KeyboardControl()));
                aiColors.ForEach(x => builder.AddAIPlayer(x, true));

                var game = builder
                      .SetUpPawns()
                      .StartingColor(TeamColor.Blue)
                      .EnableSavingToDb()
                      .ToGamePlay();


                WriterThreadStart();
                game.Start();

            }
            else if (drawGameBoard == 1)
            {
                var loadGame = GameBuilder.StartBuild()
                    .MapBoard(@"LudoORM/Map/BoardMap.txt")
                    .AddDice(new Dice(1, 6))
                    .SetInfoDisplay(ConsoleDefaults.display)
                    .LoadGame()
                    .LoadPawns(StageSaving.TeamPosition)
                    .LoadPlayers(ConsoleDefaults.KeyboardControl)
                    .StartingColor(StageSaving.Game.CurrentTurn)
                    .EnableSavingToDb()
                    .ToGamePlay();

                WriterThreadStart();
                loadGame.Start();
            }
            else if (drawGameBoard == 2)
            {
                Console.Clear();
                Console.WriteLine("Here are all the controlls for the game.\n");
                Console.WriteLine("Use arrow keys to change beween the pawns");
                Console.WriteLine("Enter is for selecting what pawn to play");
                Console.WriteLine("Press 'X' to select two pawns when you want to move out two pawns at the time \n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
                drawGameBoard = Menu.SelectedOptions(selected);
            }
        }
        private static void WriterThreadStart()
        {
            var writerThread = UiThreadBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .ToWriterThread();

            writerThread.Start();
        }
        public static void setupNoSave()
        {
            var game = GameBuilder.StartBuild()
                    .MapBoard(@"LudoORM/Map/BoardMap.txt")
                    .AddDice(new RiggedDice(new int[] { 1, 6, 2 }))
                    .SetInfoDisplay(ConsoleDefaults.display)
                    .NewGame()
                    .AddHumanPlayer(TeamColor.Blue, ConsoleDefaults.KeyboardControl())
                    .AddHumanPlayer(TeamColor.Green, ConsoleDefaults.KeyboardControl())
                    .SetUpPawns()
                    .StartingColor(TeamColor.Green)
                    .DisableSaving()
                    .ToGamePlay();

            WriterThreadStart();
            game.Start();
        }
    }
}