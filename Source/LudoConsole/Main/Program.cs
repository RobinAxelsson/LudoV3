using GameEngine.DbModel;
using System;
using System.Globalization;

namespace LudoConsole
{
    class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt"); //add connectionstring at this path and exact name to work with git ignore
        }
        static void Main(string[] args)
        {

        }
    }
}