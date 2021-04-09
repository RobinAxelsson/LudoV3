﻿using System;
using System.Collections.Generic;
using System.Globalization;
using LudoEngine.GameLogic;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Models;
namespace LudoConsole.Main
{
    internal static class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt"); //add connectionstring at this path and exact name to work with git ignore
        }

        private static void Main33(string[] args)
        {
            //Console.WriteLine(DatabaseManagement.ConnectionString);
            int selected = Menu.ShowMenu("Your turn \n", new string[] { "Roll Dice" });
            Menu.SelectedOptions(selected);
        }
    }
}