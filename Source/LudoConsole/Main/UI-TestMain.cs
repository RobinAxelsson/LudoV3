
using System.Globalization;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Creation;
using LudoEngine.Enum;
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
            //var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            //var drawGameBoard = Menu.SelectedOptions(selected);


            var builder = GameBuilder.StartBuild()
                .MapBoard(@"LudoORM/Map/BoardMap.txt")
                .AddDice(new RiggedDice(new int[] { 1, 3, 6 }))
                .SetControl(ConsoleDefaults.KeyboardControl)
                .SetInfoDisplay(ConsoleDefaults.display)
                .NewGame();

            var game = builder.AddAIPlayer(TeamColor.Blue, true)
                  .AddAIPlayer(TeamColor.Green, true)
                  .AddHumanPlayer(TeamColor.Red)
                  .AddHumanPlayer(TeamColor.Yellow)
                  .SetUpPawns()
                  .StartingColor(TeamColor.Blue)
                  .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
                  .ToGamePlay();

            //var startPoint = new PawnSavePoint()
            //{
            //    Color = TeamColor.Blue,
            //    XPosition = 4,
            //    YPosition = 0
            //};
            //var startPoint2 = new PawnSavePoint()
            //{
            //    Color = TeamColor.Red,
            //    XPosition = 5,
            //    YPosition = 0
            //};

            //var loadGame = GameBuilder.StartBuild()
            //    .MapBoard(@"@"LudoORM/Map/BoardMap.txt"")
            //    .AddDice(new Dice(1, 6))
            //    .SetControl(ConsoleDefaults.KeyboardControl)
            //    .SetInfoDisplay(ConsoleDefaults.display)
            //    .LoadPawns(new List<PawnSavePoint> { startPoint, startPoint2 })
            //    .LoadPlayers()
            //    .StartingColor(TeamColor.Blue)
            //    .GameRunsWhile(Board.IsMoreThenOneTeamLeft)
            //    .ToGamePlay();

            var writerThread = UiBuilder.StartBuild()
                .ColorSettings(UiControl.SetDefault)
                .DrawBoardConvert(Board.BoardSquares)
                .LoopCondition(() => true)
                .ToWriterThread();

            writerThread.Start();

            //loadGame.Start(DatabaseManagement.Save);
            game.Start(null);//(DatabaseManagement.Save);
        }
    }
}