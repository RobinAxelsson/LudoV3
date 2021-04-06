using System;
using System.Collections.Generic;
using System.Globalization;
using LudoConsole.UI;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoConsole.UI.Screens;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        public static List<ISquareDrawable> DrawSquares;
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DrawSquares = UiControl.ConvertAllSquares(Board.BoardSquares);
            GameSetup.NewGame(Board.BoardSquares, players: 4);
            UiControl.SetDefault();
        }
        private static void Main(string[] args)
        {
            ConsoleWriter.UpdateBoard(DrawSquares);

            while (true)
            {
                
                var color = ActivePlayer.CurrentTeam();
                int dieRoll = 1;//Dice.RollDice();
                var pawnsToMove = GameRules.SelectablePawns(color, dieRoll);
                if(pawnsToMove.Count > 0)
                    ActivePlayer.MovePawn(pawnsToMove[0]);
                ActivePlayer.NextTeam();
                ConsoleWriter.UpdateBoard(DrawSquares);
            }
        }

    }
}