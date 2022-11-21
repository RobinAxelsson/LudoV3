using System;
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.Exceptions;
using LudoEngine.GameLogic;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Creation;
using LudoEngine.GameLogic.Dice;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Main;

namespace LudoConsole.Main
{
    public enum MainMenuOptions
    {
        NewGame = 0,
        LoadGame = 1,
        Controls = 2,
        Exit = 3
    }

    internal static class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private static void Main(string[] args)
        {
            
            while(true)
            {
                var selectedOption = (MainMenuOptions)Menu.DisplayMainMenuGetSelection();

                switch (selectedOption)
                {
                    case MainMenuOptions.NewGame:
                    {
                        SetUpPlayers();
                        StartNewGame();
                        break;
                    }
                    case MainMenuOptions.LoadGame:
                    {
                        var games = DatabaseManagement.GetGames();
                        var savedGames = new List<string>();

                        if (games.Count > 0)
                        {
                            foreach (var item in games)
                            {
                                savedGames.Add(item.LastSaved.ToString("yyy/MM/dd HH:mm"));
                            }
                        }
                        else
                        {
                            savedGames.Add("You have no saved games.");
                        }
                
                        var selectedGame = Menu.ShowMenu("Select save: \n", savedGames.ToArray());
                        Console.Clear();
                        StageSaving.Game = games.ToArray()[selectedGame];

                        StageSaving.TeamPosition = DatabaseManagement.GetPawnPositionsInGame(StageSaving.Game);

                        LoadGame();
                        break;
                    }

                    case MainMenuOptions.Controls:
                        DisplayControls();
                        break;

                    case MainMenuOptions.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        throw new Exceptions.LudoConsoleException("Options out of range.");
                }
            }
        }

        private static void SetUpPlayers()
        {
            var playerCount = Menu.AskForNumberOfHumanPlayers();
            var availableColors = Menu.AskForColorSelection(playerCount);
            AddRemainingAis(playerCount - 4, availableColors);
            Console.Clear();
        }

        private static void AddRemainingAis(int numberOfAis, string[] availableColors)
        {
            if (numberOfAis != 0)
            {
                foreach (var item in availableColors)
                {
                    var colorAdd = item == "blue" ? TeamColor.Blue :
                        item == "Red" ? TeamColor.Red :
                        item == "Green" ? TeamColor.Green :
                        TeamColor.Yellow;
                    Menu.AiColor.Add(colorAdd);
                }
            }
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

        private static void DisplayControls()
        {
            Console.Clear();
            Console.WriteLine("Here are all the controls for the game.\n");
            Console.WriteLine("Use arrow keys to change between the pawns");
            Console.WriteLine("Enter is for selecting what pawn to play");
            Console.WriteLine("Press 'X' to select two pawns when you want to move out two pawns at the time \n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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

    internal class LudoConsoleException : Exception
    {
    }
}