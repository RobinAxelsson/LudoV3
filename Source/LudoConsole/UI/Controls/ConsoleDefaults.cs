using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;

namespace LudoConsole.UI.Controls
{
    public static class ConsoleDefaults
    {
        public static IInfoDisplay display { get; set; }
        public static IController KeyboardControl() => new KeyboardController();
        static ConsoleDefaults()
        {
            display = new InfoDisplay(0, 9);
        }
    }
}
