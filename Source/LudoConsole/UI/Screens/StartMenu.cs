using LudoConsole.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Screens
{
    public static class StartMenu
    {
        public enum Option1
        {
            NewGame,
            LoadGame,
            Exit
        }
        public enum Option2
        {
            _1player,
            _2player,
            _3player,
            _4player
        }
        public enum Option3
        {
            EnableBots,
            DisableBots
        }

        private const string _filePath1 = @"UI/Map/1.1 Menu.txt";
        private const string _filePath2 = @"UI/Map/1.2 ChoosePlayers.txt";
        private const string _filePath3 = @"UI/Map/1.3 Bot.txt";

        public static Option1 Menu1()
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(_filePath1);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option1>(UiControl.DefaultForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new[] { Option1.NewGame, Option1.LoadGame, Option1.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
        public static Option2 Menu2()
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(_filePath2);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option2>(UiControl.DefaultForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new[] { Option2._1player, Option2._2player, Option2._3player, Option2._4player });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
        public static Option3 Menu3()
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(_filePath2);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option3>(UiControl.DefaultForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new[] { Option3.EnableBots, Option3.DisableBots });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
    }
}
