using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LudoConsole.UI.Controls
{
    public class KeyboardEventController : IEventController
    {
        public event Action SelectionUpEvent;
        public event Action SelectionDownEvent;
        public event Action TakeOutTwoPressEvent;
        public event Action OnConfirmEvent;

        public void Activate()
        {
            var key = new ConsoleKeyInfo().Key;
            if (key == ConsoleKey.X) TakeOutTwoPressEvent?.Invoke();
            if (key == ConsoleKey.Enter) OnConfirmEvent?.Invoke();
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow) SelectionUpEvent?.Invoke();
            if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow) SelectionDownEvent?.Invoke();
        }
    }
}