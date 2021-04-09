
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

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        public static List<ISquareDrawable> DrawSquares;
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Board.Init();
            DrawSquares = UiControl.ConvertAllSquares(Board.BoardSquares);
            UiControl.SetDefault();
        }
        private static void Main(string[] args)
        {
            var writerThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    ConsoleWriter.UpdateBoard(DrawSquares);
                    Thread.Sleep(200);
                }
            }));
            var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] { "New Game", "Load Game", "Controls", "Exit" });
            var drawGameBoard = Menu.SelectedOptions(selected);
            if (drawGameBoard == 0)
            {
                var game = game2Human2AI();
                DatabaseManagement.SaveInit(game);
                writerThread.Start();
                game.Start();
            }
            else if (drawGameBoard == 1)
            {
                var game = LoadedSetupHumans();
                DatabaseManagement.SaveInit(game);
                writerThread.Start();
                game.Start();
            }
        }
        public static GamePlay LoadedSetupHumans()
        {
            var colors = GameSetup.Load(Board.BoardSquares, StageSaving.TeamPosition);

            var builder = new GamePlayBuilder(ConsoleBuild.display, ConsoleBuild.KeyboardControl);
            builder.AddStandardDice();
            colors.ForEach(x => builder.AddHuman(x));
            return builder.Play();
        }
        public static GamePlay game2Human2AI()
        {
            GameSetup.NewGame(Board.BoardSquares);

            var gamePlay = new GamePlayBuilder(ConsoleBuild.display, ConsoleBuild.KeyboardControl)
                .AddStandardDice()
                .AddAI(TeamColor.Green)
                .AddAI(TeamColor.Yellow)
                .AddHuman(TeamColor.Red)
                .AddHuman(TeamColor.Blue)
                .Play();

            return gamePlay;
        }
    }
}