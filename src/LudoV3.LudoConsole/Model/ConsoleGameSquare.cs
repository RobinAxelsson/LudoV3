using System.Collections.Generic;
using LudoConsole.Enums;

namespace LudoConsole.Model
{
    public sealed class ConsoleGameSquare
    {
        public bool IsBase { get; init; }
        public int BoardX { get; init; }
        public int BoardY { get; init; }
        public List<ConsolePawn> Pawns { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }
}