
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Creation;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Dice;
using LudoEngine.Models;

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
            //var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            //var drawGameBoard = Menu.SelectedOptions(selected);


            //var builder = GameBuilder.StartBuild()
            //    .MapBoard(@"LudoORM/Map/BoardMap.txt")
            //    .AddDice(new RiggedDice(new int[] { 1, 3, 6 }))
            //    .SetControl(ConsoleDefaults.KeyboardControl)
            //    .SetInfoDisplay(ConsoleDefaults.display)
            //    .NewGame();

            //var game = builder.AddAIPlayer(TeamColor.Blue, true)
            //      .AddAIPlayer(TeamColor.Green, true)
            //      .AddHumanPlayer(TeamColor.Red)
            //      .AddHumanPlayer(TeamColor.Yellow)
            //      .SetUpPawns()
            //      .StartingColor(TeamColor.Blue)
            //      .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
            //      .ToGamePlay();

            var startPoint = new PawnSavePoint()
            {
                Color = TeamColor.Blue,
                XPosition = 4,
                YPosition = 0
            };
            var startPoint2 = new PawnSavePoint()
            {
                Color = TeamColor.Red,
                XPosition = 5,
                YPosition = 0
            };

            var loadGame = GameBuilder.StartBuild()
                .MapBoard(@"LudoORM/Map/BoardMap.txt")
                .AddDice(new RiggedDice(new[] { 7 }))
                .SetControl(ConsoleDefaults.KeyboardControl)
                .SetInfoDisplay(ConsoleDefaults.display)
                .LoadGame()
                .LoadPawns(new List<PawnSavePoint> { startPoint, startPoint2 })
                .LoadPlayers()
                .StartingColor(TeamColor.Blue)
                .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
                .DisableSaving()
                .ToGamePlay();

            var writerThread = UiThreadBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .StopEventFrom(loadGame)
                .ToWriterThread();
            
            writerThread.Start();
            loadGame.Start();
        }
    }
}