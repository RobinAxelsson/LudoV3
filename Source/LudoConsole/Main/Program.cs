using System;
using System.Globalization;
using System.Linq;
using LudoEngine.GameLogic;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Creation;
using LudoEngine.GameLogic.Dice;
using LudoConsole.UI.Controls;
using LudoEngine;
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
            
            while(true)
            {
                var selectedOption = (MainMenuOptions)Menu.DisplayMainMenuGetSelection();

                switch (selectedOption)
                {
                    case MainMenuOptions.Test:
                    {
                        Console.Clear();
                        LudoEngineFacade.SetBoard();
                        WriterThreadStart();

                        Console.ReadKey();
                        break;
                    }

                    case MainMenuOptions.NewGame:
                    {
                        var playerCount = Menu.AskForNumberOfHumanPlayers();
                        var teamColors = Menu.SetColorSelection(playerCount);

                        Console.Clear();
                        
                        var builder = GameBuilder.StartBuild();
                        builder.MapBoard(@"LudoORM/Map/BoardMap.txt");
                        builder.AddDice(new Dice(1, 6));
                        
                        builder.NewGame();

                        teamColors.human.ToList().ForEach(x => builder.AddHumanPlayer(x, ConsoleDefaults.KeyboardControl));
                        teamColors.ai.ToList().ForEach(x => builder.AddAIPlayer(x, true));

                        builder.SetUpPawns();
                        builder.StartingColor(TeamColor.Blue);
                        builder.DisableSaving();

                        var game = builder.ToGamePlay();
                        
                        WriterThreadStart();
                        game.Start();
                        break;
                    }
                    case MainMenuOptions.LoadGame:
                    {
                        var savedGames = LudoEngineFacade.GetSavedGames();
                        
                        var timeSaved = savedGames.Select(x => x.LastSaved.ToString("yyy/MM/dd HH:mm")).ToArray();
                        
                        var gameIndex = Menu.ShowMenu("Saved games: \n", timeSaved);
                        Console.Clear();

                        var game = savedGames[gameIndex];
                        var savingDto = LudoEngineFacade.GetStageSavingDto(game.Id);

                        //StageSaving.TeamPosition = DatabaseManagement.GetPawnPositionsInGame(StageSaving.Game);

                        var gameBuilder = GameBuilder.StartBuild();
                        gameBuilder.MapBoard(@"LudoORM/Map/BoardMap.txt");

                        WriterThreadStart();


                        gameBuilder.AddDice(new Dice(1, 6));
                        //gameBuilder.SetInfoDisplay(ConsoleDefaults.Display);
                        gameBuilder.LoadGame();
                        gameBuilder.LoadPawns(savingDto.TeamPosition);
                        gameBuilder.LoadPlayers(ConsoleDefaults.KeyboardControl);
                        gameBuilder.StartingColor(savingDto.Game.CurrentTurn);
                        gameBuilder.EnableSavingToDb();
                        
                        var loadGame = gameBuilder.ToGamePlay();

                        loadGame.Start();
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

        //private static void SetUpPlayers()
        //{
        //    var playerCount = Menu.AskForNumberOfHumanPlayers();
        //    var availableColors = Menu.SetColorSelection(playerCount);
        //    AddRemainingAis(playerCount - 4, availableColors);
        //    Console.Clear();
        //}

        //private static void AddRemainingAis(int numberOfAis, string[] availableColors)
        //{
        //    if (numberOfAis != 0)
        //    {
        //        foreach (var item in availableColors)
        //        {
        //            var colorAdd = item == "blue" ? TeamColor.Blue :
        //                item == "Red" ? TeamColor.Red :
        //                item == "Green" ? TeamColor.Green :
        //                TeamColor.Yellow;
        //            Menu.AiColor.Add(colorAdd);
        //        }
        //    }
        //}

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
            var boardRenderer = new BoardRenderer(Board.BoardSquares);
            boardRenderer.Start();
        }
        public static void SetupNoSave()
        {
            var game = GameBuilder.StartBuild()
                    .MapBoard(@"LudoORM/Map/BoardMap.txt")
                    .AddDice(new RiggedDice(new [] { 1, 6, 2 }))
                    
                    .NewGame()
                    .AddHumanPlayer(TeamColor.Blue, ConsoleDefaults.KeyboardControl)
                    .AddHumanPlayer(TeamColor.Green, ConsoleDefaults.KeyboardControl)
                    .SetUpPawns()
                    .StartingColor(TeamColor.Green)
                    .DisableSaving()
                    .ToGamePlay();

            WriterThreadStart();
            game.Start();
        }
    }
}