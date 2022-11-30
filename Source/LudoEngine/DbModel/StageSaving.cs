using LudoEngine.Models;
using System.Collections.Generic;
using LudoEngine.GameLogic;

namespace LudoEngine.DbModel
{
    internal static class StageSaving {
        public static List<Pawn> Pawns { get; set;}

        public static Game Game { get; set; }

        public static List<Player> Players { get; set; }

        public static List<PawnSavePoint> TeamPosition { get; set; }

        public static int CurrentTeam { get; set; }
    }

    internal sealed class StageSavingDto
    {
        public  List<Pawn> Pawns { get; init; }

        public  Game Game { get; init; }

        public  List<Player> Players { get; init; }

        public  List<PawnSavePoint> TeamPosition { get; init; }

        public  int CurrentTeam { get; init; }
    }
}