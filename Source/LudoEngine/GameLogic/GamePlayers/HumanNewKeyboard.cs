using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;
using LudoEngine.Models;

namespace LudoEngine.GameLogic.GamePlayers
{
    public class HumanNewKeyboard
    {
        private IEventController Controller { get; set; }
        public TeamColor Color { get; set; }
        public static event Action<HumanPlayer, int> HumanThrowEvent;
        public static event Action<HumanPlayer> OnTakeOutTwoPossibleEvent;
        public HumanNewKeyboard(TeamColor color, IEventController eventController)
        {
            Controller = eventController;
            Color = color;
            Controller.SelectionDownEvent += ChangeSelectionDown;
            Controller.SelectionUpEvent += ChangeSelectionUp;
            //Controller.OnConfirmEvent += 
        }
        private List<Pawn> _pawnsToMove { get; set; } = new();
        private int _pawnIndex { get; set; } = 0;
        private void ChangeSelectionUp()
        {
            _pawnIndex++;
            _pawnIndex =
                    _pawnIndex > _pawnsToMove.Count - 1 ? 0 :
                    _pawnIndex < 0 ? _pawnsToMove.Count - 1 : _pawnIndex;
            Select(_pawnIndex);
            Controller.Activate();
        }
        private void ChangeSelectionDown()
        {
            _pawnIndex--;
            _pawnIndex =
                    _pawnIndex > _pawnsToMove.Count - 1 ? 0 :
                    _pawnIndex < 0 ? _pawnsToMove.Count - 1 : _pawnIndex;
            Select(_pawnIndex);
            Controller.Activate();
        }
        private void Select(int pawnIndex)
        {
            DeselectAll();
            _pawnsToMove[pawnIndex].IsSelected = true;
        }
        private void DeselectAll() => _pawnsToMove.ForEach(x => x.IsSelected = false);
        private void TakeOutTwo()
        {
            for (int i = 0; i < 2; i++)
                Board.PawnsInBase(Color)[0].Move(1);
            Controller.TakeOutTwoPressEvent -= TakeOutTwo;
        }
        public void Play(IDice dice)
        {
            int result = dice.Roll();

            _pawnsToMove = GameRules.SelectablePawns(Color, result);
            if (_pawnsToMove.Count == 0) return;

            if (GameRules.CanTakeOutTwo(Color, result))
                Controller.TakeOutTwoPressEvent += TakeOutTwo;
            Controller.Activate();



            //select [0] pawn

            //x event available
            //select basepawns move[0]
            //

            //select event allways
            //change selection = true pawn
            //confirm event allways
            //move pawn

            //var selected = Control.Select(selectablePawns, takeoutTwoIsOption);

            //if (selected.Count == 2)
            //{
            //    selected[0].Move(1);
            //    selected[1].Move(1);
            //    tookOutTwo = true;
            //}
            //if (selected.Count == 1)
            //{
            //    selected[0].Move(result);
            //}

            //if (result == 6 && tookOutTwo == false) Play(dice);
        }

    }
}
