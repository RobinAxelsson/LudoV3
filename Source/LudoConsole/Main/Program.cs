using System;
using System.Globalization;
using LudoEngine.GameLogic;
using LudoConsole.UI.Controls;
using LudoEngine;

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
                    case MainMenuOptions.TestRender:
                    {
                        Console.Clear();
                        var facadeSquares = LudoEngineFacade.GetNewGameBoardSquares();
                        var squares = ConsoleDtoMapping.Map(facadeSquares);
                        BoardRenderer.StartRender(squares);

                        Console.ReadKey();
                        break;
                    }

                    case MainMenuOptions.NewGame:
                    {
                        //var playerCount = Menu.AskForNumberOfHumanPlayers();
                        //var teamColors = Menu.SetColorSelection(playerCount);

                        //Console.Clear();
                        
                        //var builder = GameBuilder.StartBuild();
                        
                        //LudoEngineFacade.SetBoard();
                        
                        //builder.AddDice(new Dice(1, 6));
                        
                        //builder.NewGame();

                        //teamColors.human.ToList().ForEach(x => builder.AddHumanPlayer(x, ConsoleDefaults.KeyboardControl));
                        //teamColors.ai.ToList().ForEach(x => builder.AddAIPlayer(x, true));

                        //builder.SetUpPawns();
                        //builder.StartingColor(TeamColor.Blue);
                        //builder.DisableSaving();

                        //var game = builder.ToGamePlay();

                        //BoardRenderer.StartRender(StaticBoard.BoardSquares);
                        //game.Start();

                        break;
                    }
                    case MainMenuOptions.LoadGame:
                    {
                            //var savedGames = LudoEngineFacade.GetSavedGames();

                            //var timeSaved = savedGames.Select(x => x.LastSaved.ToString("yyy/MM/dd HH:mm")).ToArray();

                            //var gameIndex = Menu.ShowMenu("Saved games: \n", timeSaved);
                            //Console.Clear();

                            //var game = savedGames[gameIndex];
                            //var savingDto = LudoEngineFacade.GetStageSavingDto(game.Id);

                            //var gameBuilder = GameBuilder.StartBuild();
                            //gameBuilder.MapBoard(@"LudoORM/Map/BoardMap.txt");

                            //BoardRenderer.StartRender(StaticBoard.BoardSquares);

                            //gameBuilder.AddDice(new Dice(1, 6));
                            //gameBuilder.LoadGame();
                            //gameBuilder.LoadPawns(savingDto.TeamPosition);
                            //gameBuilder.LoadPlayers(ConsoleDefaults.KeyboardControl);
                            //gameBuilder.StartingColor(savingDto.Game.CurrentTurn);
                            //gameBuilder.EnableSavingToDb();

                            //var loadGame = gameBuilder.ToGamePlay();

                            //loadGame.Start();
                            break;
                    }

                    case MainMenuOptions.Controls:
                        ConsoleInfo.DisplayControlInfo();
                        break;

                    case MainMenuOptions.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        throw new Exceptions.LudoConsoleException("Options out of range.");
                }
            }
        }

        //private static void LoadGame()
        //{
        //    var loadGame = GameBuilder.StartBuild()
        //        .MapBoard(@"LudoORM/Map/BoardMap.txt")
        //        .AddDice(new Dice(1, 6))
                
        //        .LoadGame()
        //        .LoadPawns(StageSaving.TeamPosition)
        //        .LoadPlayers(ConsoleDefaults.KeyboardControl)
        //        .StartingColor(StageSaving.Game.CurrentTurn)
        //        .EnableSavingToDb()
        //        .ToGamePlay();

        //    BoardRenderer.StartRender(StaticBoard.BoardSquares);
        //    loadGame.Start();
        //}
    }
}