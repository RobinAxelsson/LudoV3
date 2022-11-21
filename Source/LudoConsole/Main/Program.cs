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
            var selectedOption = Menu.AskForMainMenuSelection();
            int drawGameBoard;

            do
            {
                drawGameBoard = Menu.SelectedOptions(selectedOption);

                if (drawGameBoard == 0)
                {
                    StartNewGame();
                }
                else if (drawGameBoard == 1)
                {
                    LoadGame();
                }
                else if (drawGameBoard == 2)
                {
                    WriteControlInfo();
                    Console.ReadKey();

                    selectedOption = Menu.AskForMainMenuSelection();
                }
            } while (drawGameBoard != 3);
            
        }

        private static void StartNewGame()
        {
            var builder = GameBuilder.StartBuild()
                .MapBoard(@"LudoORM/Map/BoardMap.txt")
                .AddDice(new Dice(1, 6))
                .SetInfoDisplay(ConsoleDefaults.display)
                .NewGame();

            var humanColors = Menu.HumanColor;
            var aiColors = Menu.AiColor;

            humanColors.ForEach(x => builder.AddHumanPlayer(x, ConsoleDefaults.KeyboardControl()));
            aiColors.ForEach(x => builder.AddAIPlayer(x, true));

            var game = builder
                .SetUpPawns()
                .StartingColor(TeamColor.Blue)
                .DisableSaving()
                //.EnableSavingToDb()
                .ToGamePlay();


            WriterThreadStart();
            game.Start();
        }

        private static void WriteControlInfo()
        {
            Console.Clear();
            Console.WriteLine("Here are all the controls for the game.\n");
            Console.WriteLine("Use arrow keys to change between the pawns");
            Console.WriteLine("Enter is for selecting what pawn to play");
            Console.WriteLine("Press 'X' to select two pawns when you want to move out two pawns at the time \n");
            Console.WriteLine("Press any key to continue...");
        }

        private static void LoadGame()
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

        private static void WriterThreadStart()
        {
            var writerThread = UiThreadBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .ToWriterThread();

            writerThread.Start();
        }
        public static void SetupNoSave()
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