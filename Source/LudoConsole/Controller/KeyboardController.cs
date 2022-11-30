using System;
using LudoEngine.ClientApi;

namespace LudoConsole.Controller
{
    public class KeyboardController : IController
    {
        public event Action SelectionUpEvent;
        public event Action SelectionDownEvent;
        public event Action TakeOutTwoPressEvent;
        public event Action OnConfirmEvent;

        public void Activate()
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.X) TakeOutTwoPressEvent?.Invoke();
            if (key == ConsoleKey.Enter) OnConfirmEvent?.Invoke();
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow) SelectionUpEvent?.Invoke();
            if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow) SelectionDownEvent?.Invoke();
        }
    }
}