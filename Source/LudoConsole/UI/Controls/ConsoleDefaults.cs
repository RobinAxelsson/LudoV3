using LudoEngine.GameLogic.Interfaces;

namespace LudoConsole.UI.Controls
{
    public static class ConsoleDefaults
    {
        public static IInfoDisplay display { get; set; }
        public static IController KeyboardControl { get; set; }
        static ConsoleDefaults()
        {
            display = new InfoDisplay(0, 9);
            KeyboardControl = new KeyboardControl(display.Update);
        }
    }
}
