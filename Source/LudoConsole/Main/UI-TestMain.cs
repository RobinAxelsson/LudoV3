
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Creation;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.Dice;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }
        private static void Main(string[] args)
        {
            var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            var drawGameBoard = Menu.SelectedOptions(selected);
          
        
        var game = GameBuilder.Start()
                .MapBoard(@"BoardUnits/Map/BoardMap.txt")
                .AddDice(new RiggedDice(new int[] { 1, 3, 6 }))
                .SetControl(ConsoleDefaults.KeyboardControl)
                .SetInfoDisplay(ConsoleDefaults.display)
                .NewGame()
                .AddAIPlayer(TeamColor.Blue, true)
                .AddAIPlayer(TeamColor.Green, true)
                .AddHumanPlayer(TeamColor.Red)
                .AddHumanPlayer(TeamColor.Yellow)
                .StartingColor(TeamColor.Blue)
                .SetUpPawns()
                .GameRunsWhile(Board.IsMoreThenOneTeam)
                .ToGamePlay();

            //var loadGame = GameBuilder.Start()
            //    .MapBoard(@"BoardUnits/Map/BoardMap.txt")
            //    .AddDice(new Dice(1, 6))
            //    .SetControl(ConsoleDefaults.KeyboardControl)
            //    .SetInfoDisplay(ConsoleDefaults.display)
            //    .LoadGame()
            //    .ToGamePlay();

            var writerThread = UiBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .LoopCondition(() => true)
                .ToWriterThread();

            writerThread.Start();
            game.Start();
        }
    }
}