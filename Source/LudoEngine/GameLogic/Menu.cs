using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic
{
    public class Menu
    {
        public static int ShowMenu(string info, string[] options)
        {
            Console.CursorVisible = false;
            int selected = 0;

            HighlightMenuOption(info, options, selected);

            ConsoleKey key = Console.ReadKey().Key;

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

                key = Console.ReadKey().Key;
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

        public static void SelectedOptions(int selected)
        {
            if (selected == 0)
            {
                int diceNumber = ActivePlayer.RollDice();

                ShowMenu("You rolled " + diceNumber + "\n" + "Select Witch pawn to move", new string[] {"pawn1", "pawn2", "pawn3", "pawn4" });
            }
        }
    }
}
