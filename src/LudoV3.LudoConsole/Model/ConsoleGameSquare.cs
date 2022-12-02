using System.Collections.Generic;
using LudoConsole.Enums;

namespace LudoConsole.Model
{
    internal sealed class ConsoleGameSquare
    {
        public bool IsBase { get; init; }
        public int BoardX { get; init; }
        public int BoardY { get; init; }
        public List<ConsolePawn> Pawns { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }

    internal sealed class PawnCollection
    {
        private List<ConsolePawn> Pawns { get; init; }
        //Count, IsSelected, Color
    }
}