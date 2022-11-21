using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;

namespace LudoConsole.UI.Controls
{
    public static class ConsoleDefaults
    {
        public static IInfoDisplay Display => new InfoDisplay(0, 9);
        public static IController KeyboardControl => new KeyboardController();
    }
}
