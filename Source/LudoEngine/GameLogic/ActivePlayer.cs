using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine.GameLogic
{
    public static class ActivePlayer
    {
        static ActivePlayer() => iCurrentTeam = 0;

        private static Random random = new Random();
        private static List<TeamColor> OrderOfTeams = new List<TeamColor>
        {
            TeamColor.Blue,
            TeamColor.Red,
            TeamColor.Green,
            TeamColor.Yellow
        };
        public static void SelectPawn(Pawn pawn)
        {
            Board.BoardSquares.SelectMany(x => x.Pawns).ToList().ForEach(x => x.IsSelected = false);
            pawn.IsSelected = true;
            SelectedPawn = pawn;
        }
        public static TeamColor CurrentTeam() => OrderOfTeams[iCurrentTeam];
        private static int iCurrentTeam { get; set; }
        public static Pawn SelectedPawn { get; set; }
        public static void SetFirstTeam(TeamColor color) => iCurrentTeam = OrderOfTeams.FindIndex(x => x == color);
        public static void NextTeam() //not working with fewer players
        {
            iCurrentTeam++;
            if (iCurrentTeam % 4 == 0) iCurrentTeam = 0;
        }
        public static int RollDice() => random.Next(1, 6);
        public static List<Pawn> SelectablePawns(int rollDie) => GameRules.SelectablePawns(CurrentTeam(), rollDie);
        public static void MoveSelectedPawn(int dieRoll) 
        {
            if (SelectedPawn == null) return;
            SelectedPawn.Move(dieRoll); 
        }
    }
}
