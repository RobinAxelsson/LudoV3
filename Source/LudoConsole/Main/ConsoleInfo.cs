using System;

namespace LudoConsole.Main
{
    public static class ConsoleInfo
    {
        public static void DisplayControlInfo()
        {
            Console.Clear();
            Console.WriteLine("Here are all the controls for the game.\n");
            Console.WriteLine("Use arrow keys to change between the pawns");
            Console.WriteLine("Enter is for selecting what pawn to play");
            Console.WriteLine("Press 'X' to select two pawns when you want to move out two pawns at the time \n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}