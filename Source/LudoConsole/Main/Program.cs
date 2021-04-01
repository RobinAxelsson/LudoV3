using System;
using System.Globalization;
using LudoEngine.DbModel;
using LudoEngine.Enum;

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
            //DatabaseManagement.SavePlayer("eshag");
            DatabaseManagement.GetPlayers(1);
        }


    }
}