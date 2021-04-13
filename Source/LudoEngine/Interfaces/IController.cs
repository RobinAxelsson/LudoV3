using System;

namespace LudoEngine.Interfaces
{
    public interface IController
    {
        public void Activate();

        public event Action SelectionUpEvent;
        public event Action SelectionDownEvent;
        public event Action TakeOutTwoPressEvent;
        public event Action OnConfirmEvent;
    }
}
