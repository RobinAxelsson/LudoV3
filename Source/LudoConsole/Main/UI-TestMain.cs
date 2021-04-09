using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LudoConsole.UI;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Main;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.Dice;
using LudoEngine.GameLogic.GamePlayers;
using LudoEngine.GameLogic.Interfaces;

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
            GameSetup.NewGame(Board.BoardSquares, players: 4);
            var writerThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    ConsoleWriter.UpdateBoard(DrawSquares);
                    Thread.Sleep(200);
                }
            }));
            var selected = Menu.ShowMenu("Welcome to this awsome Ludo game! \n", new string[] {"New Game", "Load Game", "Controls", "Exit" });
            var drawGameBoard = Menu.SelectedOptions(selected);
            if (drawGameBoard == 0)
            {
                //GameSetup.NewGame(Board.BoardSquares, 4);
                var game = TempGameSetup();
                game.Start();
                writerThread.Start();
            }
            else if (drawGameBoard == 1)
            {
                GameSetup.Load(Board.BoardSquares, StageSaving.TeamPosition);
                //game.Start();
            }
        }
        public static GamePlay TempGameSetup()
        {
            GameSetup.NewGame(Board.BoardSquares, 4);
            var display = new InfoDisplay(0, 9);
            var keyboardControl = new KeyboardControl(display.Update);
            var dice = new Dice(6, 6);
            var redPlayer = new HumanPlayer(TeamColor.Red, display.UpdateDiceRoll, keyboardControl);
            var bluePlayer = new HumanPlayer(TeamColor.Blue, display.UpdateDiceRoll, keyboardControl);
            var greenPlayer = new Stephan(TeamColor.Green, display.UpdateAIDiceRoll);
            var yellowPlayer = new Stephan(TeamColor.Yellow, display.UpdateAIDiceRoll);
            return new GamePlay(new List<IGamePlayer> { redPlayer, bluePlayer, greenPlayer, yellowPlayer }, dice);
        }
    }
}