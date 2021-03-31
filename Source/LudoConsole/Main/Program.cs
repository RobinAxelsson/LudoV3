using System;
using System.Globalization;
using LudoEngine.GameLogic;
using LudoEngine.DbModel;
namespace LudoConsole.Main
{
    internal static class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt"); //add connectionstring at this path and exact name to work with git ignore
        }

        private static void Main(string[] args)
        {
            //Console.WriteLine(DatabaseManagement.ConnectionString);
            string selected = Menu.ShowMenu("Your turn \n", new string[] { "Roll Dice" });
            Menu.SelectedOptions(selected);
        }
    }
}