using System;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;

namespace LudoEngine.GameLogic.GamePlayers
{
    public class HumanPlayer : IGamePlayer
    {
        public HumanPlayer(TeamColor color, IController control)
        {
            Color = color;
            Pawns = Board.GetTeamPawns(color);
            Control = control;
        }
        public IController Control { get; set; }
        public static event Action<HumanPlayer, int> HumanThrowEvent;
        public static event Action<HumanPlayer> OnTakeOutTwoPossibleEvent;
        public List<Pawn> Pawns { get; set; }
        public TeamColor Color { get; set; }
        public void Play(IDice dice)
        {
            int result = dice.Roll();
            bool tookOutTwo = false;

            HumanThrowEvent?.Invoke(this, result);
            var selectablePawns = GameRules.SelectablePawns(Color, result);
            if (selectablePawns.Count == 0) return;

            bool takeoutTwoIsOption = GameRules.CanTakeOutTwo(Color, result);
            if (takeoutTwoIsOption) OnTakeOutTwoPossibleEvent?.Invoke(this);
            var selected = Control.Select(selectablePawns, takeoutTwoIsOption);

            if(selected.Count == 2)
            {
                selected[0].Move(1);
                selected[1].Move(1); //0, 1 on selected
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