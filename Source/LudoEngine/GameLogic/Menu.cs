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
        public static List<TeamColor> humanColor = new();
        public static List<TeamColor> aiColor = new();

        public static int ShowMenu(string info, object[] options)
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

        public static void HighlightMenuOption(string info, object[] options, int index)
        {
            //Clear the console so it doesn't print the new values on new lines, but instead replaces current values with new values on respective line
            Console.Clear();

            //print info once again
            Console.WriteLine(info);

            for (int i = 0; i < options.Length; i++)
            {
                //if i equals the index value we are highlighting, print it in green color with an arrow to show that THIS is the value we are on
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("> " + options[i]);
                    //reset text color back to gray
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                //else simply print the value
                else
                {
                    Console.WriteLine(options[i]);
                }
            }
        }

        public static int SelectedOptions(int selected)
        {
            if (selected == 0)
            {
                Console.WriteLine("Write a number");
                Console.Write("How many players are you: ");
                int players = Convert.ToInt32(Console.ReadLine());
                string[] selectebleColors = new string[] { "Blue", "Red", "Green", "Yellow" };
                
                for (int i = 0; i < players; i++)
                {
                    int removeIdex = ShowMenu("Select player color: \n", selectebleColors);
                    var colorAdd = selectebleColors[removeIdex] == "Blue" ? TeamColor.Blue :
                        selectebleColors[removeIdex] == "Red" ? TeamColor.Red :
                        selectebleColors[removeIdex] == "Green" ? TeamColor.Green :
                        TeamColor.Yellow;
                    humanColor.Add(colorAdd);
                    selectebleColors = selectebleColors.Where((source, index) => index != removeIdex).ToArray();
                }

                int numberOfAis = Convert.ToInt32(players) - 4;
                if (numberOfAis != 0)
                {
                    foreach (var item in selectebleColors)
                    {
                        var colorAdd = item == "blue" ? TeamColor.Blue :
                        item == "Red" ? TeamColor.Red :
                        item == "Green" ? TeamColor.Green :
                        TeamColor.Yellow;
                        aiColor.Add(colorAdd);
                    }
                }
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
    }
}
