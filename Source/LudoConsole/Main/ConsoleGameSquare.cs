using System.Collections.Generic;

namespace LudoConsole.Main
{
    public sealed class ConsoleGameSquare
    {
        public bool IsBase { get; init; }
        public int BoardX { get; init; }
        public int BoardY { get; init; }
        public List<ConsolePawnDto> Pawns { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }

    public enum ConsoleTeamColor
    {
        Blue,
        Red,
        Yellow,
        Green,
        Default
    }
}