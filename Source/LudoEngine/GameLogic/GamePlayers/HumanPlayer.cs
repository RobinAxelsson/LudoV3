using System;
using System.Collections.Generic;
using System.Threading;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    public class HumanPlayer : IGamePlayer
    {
        public HumanPlayer(TeamColor color, Action<TeamColor, int, Action> displayDice, IController control)
        {
            Color = color;
            DisplayDice = displayDice;
            Pawns = Board.GetTeamPawns(color);
            Control = control;
        }
        public IController Control { get; set; }
        private Action<TeamColor, int, Action> DisplayDice { get; set; }
        public List<Pawn> Pawns { get; set; }
        public TeamColor Color { get; set; }
        public void Play(IDice dice)
        {
            int result = dice.Roll();
            bool tookOutTwo = false;
            DisplayDice(Color, result, () => Console.ReadKey(true));
            var selectablePawns = GameRules.SelectablePawns(Color, result);
            if (selectablePawns.Count == 0) return;

            var selected = Control.Select(selectablePawns, GameRules.CanTakeOutTwo(Color, result));

            if(selected.Count == 2)
            {
                selected[0].Move(1);
                selected[1].Move(1);
                tookOutTwo = true;
            }
            if (selected.Count == 1)
            {
               selected[0].Move(result);
            }

            if (result == 6 && tookOutTwo == false) Play(dice);
        }
    }
}