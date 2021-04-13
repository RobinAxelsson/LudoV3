using System;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;
using LudoEngine.Models;

namespace LudoEngine.GameLogic.GamePlayers
{
    public class HumanPlayer : IGamePlayer
    {
        private IController Controller { get; set; }
        public static event Action<IGamePlayer, int> HumanThrowEvent;
        public static event Action<IGamePlayer> OnTakeOutTwoPossibleEvent;
        public TeamColor Color { get; set; }
        private int _result { get; set; }
        private List<Pawn> _pawnsToMove { get; set; } = new();
        private int _pawnIndex { get; set; }
        private IDice _dice { get; set; }
        private bool _tookOutTwo { get; set; }
        public HumanPlayer(TeamColor color, IController eventController)
        {
            Controller = eventController;
            Color = color;
            Controller.SelectionDownEvent += ChangeSelectionDown;
            Controller.SelectionUpEvent += ChangeSelectionUp;
            Controller.OnConfirmEvent += MoveSelectedPawn;
        }
        public void Play(IDice dice)
        {
            int pawnIndex = 0;
            int result = dice.Roll();
            var pawnsToMove = GameRules.SelectablePawns(Color,result);

            _dice = dice;
            _pawnIndex = pawnIndex;
            _result = result;
            _pawnsToMove = pawnsToMove;

            HumanThrowEvent?.Invoke(this, _result);
            if (pawnsToMove.Count == 0) return;
            pawnsToMove[0].IsSelected = true;

            if (GameRules.CanTakeOutTwo(Color, _result))
            {
                OnTakeOutTwoPossibleEvent?.Invoke(this);
                Controller.TakeOutTwoPressEvent += TakeOutTwo;
            }
            Controller.Activate();
            if (_tookOutTwo == false && result == 6)
            {
                Controller.TakeOutTwoPressEvent -= TakeOutTwo;
                Play(dice);
            }
        }
        private void PlayAgain()
        {
            Controller.OnConfirmEvent -= PlayAgain;
            Play(_dice);
        }

        private void MoveSelectedPawn()
        {
            _pawnsToMove[_pawnIndex].Move(_result);
        }
        private void ChangeSelectionUp()
        {
            var tempIndex = _pawnIndex + 1;
            if (tempIndex > _pawnsToMove.Count - 1)
                tempIndex = 0;

            _pawnIndex = tempIndex;
            Select(tempIndex);
            Controller.Activate();
        }
        private void ChangeSelectionDown()
        {
            var tempIndex = _pawnIndex - 1;
            if (tempIndex < 0)
                tempIndex = _pawnsToMove.Count - 1;

            _pawnIndex = tempIndex;
            Select(tempIndex);
            Controller.Activate();
        }
        private void Select(int pawnIndex)
        {
            DeselectAll();
            _pawnsToMove[pawnIndex].IsSelected = true;
        }

        private void TakeOutTwo()
        {
            for (int i = 0; i < 2; i++)
                Board.PawnsInBase(Color)[0].Move(1);
            _tookOutTwo = true;
            Controller.TakeOutTwoPressEvent -= TakeOutTwo;
        }

        private void DeselectAll() => _pawnsToMove.ForEach(x => x.IsSelected = false);
    }
}