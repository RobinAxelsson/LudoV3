using System.Collections.Generic;

namespace LudoConsole.ServerMapping
{
    public sealed class DtoConsoleGameSquare
    {
        public bool IsBase { get; init; }
        public int BoardX { get; init; }
        public int BoardY { get; init; }
        public List<DtoConsolePawn> Pawns { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }
}