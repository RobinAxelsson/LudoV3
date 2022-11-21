using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.DbModel;
using LudoEngine.Models;

namespace LudoEngine.GameLogic
{
    public static class Menu
    {
        public static List<TeamColor> HumanColor { get; } = new();
        public static List<TeamColor> AiColor { get; } = new();

        public static int DisplayMainMenuGetSelection()
        {
            return ShowMenu("Welcome to this awesome Ludo game! \n",
                new[] {"New Game", "Load Game", "Controls", "Exit"});
        }

        private static int ShowMenu(string info, string[] options)
        {
            Console.CursorVisible = false;
            int selected = 0;

            HighlightMenuOption(info, options, selected);

            ConsoleKey key = Console.ReadKey(true).Key;

            while (key != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow && selected > 0)
                {
                    selected--;
                    HighlightMenuOption(info, options, selected);
                }
                else if (key == ConsoleKey.DownArrow && selected < options.Length - 1)
                {
                    selected++;
                    HighlightMenuOption(info, options, selected);
                }

                key = Console.ReadKey(true).Key;
            }

            return selected;

        }

        public static int SelectedOptions(int selected)
        {
            if (selected == 0)
            {
                var players = AskForNumberOfHumanPlayers();
                var availableColors = AskForColorSelection(players);
                
                var numberOfAis = players - 4;
                if (numberOfAis != 0)
                    AddRemainingColorsAsAi(numberOfAis, availableColors);

                Console.Clear();


                return 0;
            }
            else if (selected == 1)
            {
                //Gets the Saved games
                List<Game> games = DatabaseManagement.GetGames();
                List<string> savedGames = new ();
                //Lists the games if there are any saved games
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
                
                int selectedGame = ShowMenu("Select save: \n", savedGames.ToArray());
                Console.Clear();
                //Sets the stageSaving class so we can access the game later
                StageSaving.Game = games.ToArray()[selectedGame];

                //Gets the pawn positions for the selected game and saves them to the stageSaving class
                StageSaving.TeamPosition = DatabaseManagement.GetPawnPositionsInGame(StageSaving.Game);

                return 1;
            }
            else if (selected == 2)
            {
                return 2;
            }
            else
            {
                Environment.Exit(0);
                return 3;
            }
        }

        private static void AddRemainingColorsAsAi(int numberOfAis, string[] availableColors)
        {
                foreach (var item in availableColors)
                {
                    var colorAdd = item == "blue" ? TeamColor.Blue :
                        item == "Red" ? TeamColor.Red :
                        item == "Green" ? TeamColor.Green :
                        TeamColor.Yellow;
                    AiColor.Add(colorAdd);
                }
        }

        private static string[] AskForColorSelection(int players)
        {
            var selectableColors = new[] {"Blue", "Red", "Green", "Yellow"};

            for (var i = 0; i < players; i++)
            {
                var removeIndex = ShowMenu("Select player color: \n", selectableColors);
                var colorAdd = selectableColors[removeIndex] == "Blue" ? TeamColor.Blue :
                    selectableColors[removeIndex] == "Red" ? TeamColor.Red :
                    selectableColors[removeIndex] == "Green" ? TeamColor.Green :
                    TeamColor.Yellow;
                HumanColor.Add(colorAdd);
                selectableColors = selectableColors.Where((source, index) => index != removeIndex).ToArray();
            }

            return selectableColors;
        }

        private static int AskForNumberOfHumanPlayers()
        {
            Console.Clear();
            var playerCount = ShowMenu("How many players are you: ", new[] {"1", "2", "3", "4"}) + 1;
            return playerCount;
        }

        private static void HighlightMenuOption(string info, IReadOnlyList<string> options, int index)
        {
            Console.Clear();

            Console.WriteLine(info);

            for (var i = 0; i < options.Count; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("> " + options[i]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine(options[i]);
                }
            }
        }
    }
}
