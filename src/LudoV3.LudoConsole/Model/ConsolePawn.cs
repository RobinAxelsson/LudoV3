using LudoConsole.Enums;

namespace LudoConsole.Model
{
    public class ConsolePawn
    {
        public int Id { get; init; }
        public bool IsSelected { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }
}