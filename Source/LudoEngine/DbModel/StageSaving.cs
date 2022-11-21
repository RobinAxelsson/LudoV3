using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.DbModel
{
    public static class StageSaving {
        public static List<Pawn> Pawns { get; set;}

        public static Game Game { get; set; }

        public static List<Player> Players { get; set; }

        public static List<PawnSavePoint> TeamPosition { get; set; }

        public static int CurrentTeam { get; set; }
    }

    public sealed class StageSavingDto
    {
        public  List<Pawn> Pawns { get; init; }

        public  Game Game { get; init; }

        public  List<Player> Players { get; init; }

        public  List<PawnSavePoint> TeamPosition { get; init; }

        public  int CurrentTeam { get; init; }
    }
}