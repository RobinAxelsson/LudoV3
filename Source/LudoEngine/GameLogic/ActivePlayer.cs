using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic
{
    public static class ActivePlayer
    {
        static ActivePlayer()
        {
            iCurrentTeam = 0;
        }
        private static List<TeamColor> OrderOfTeams = new List<TeamColor>
        {
            TeamColor.Blue,
            TeamColor.Red,
            TeamColor.Green,
            TeamColor.Yellow
        };
        public static TeamColor CurrentTeam() => OrderOfTeams[iCurrentTeam];
        private static int iCurrentTeam { get; set; }
        private static Pawn SelectedPawn { get; set; }
        private static int DiceNumber { get; set; }
        public static void SetFirstTeam(TeamColor color) => iCurrentTeam = OrderOfTeams.FindIndex(x => x == color);
        public static void NextTeam()
        {
            iCurrentTeam++;
            if (iCurrentTeam % 4 == 0) iCurrentTeam = 0;
        }
        public static List<Pawn> RollDice()
        {
            var moveablePawns = new List<Pawn>();
            DiceNumber = Dice.RollDice();
            return GameRules.SelectablePawns(CurrentTeam(), DiceNumber);
        }
        public static void MovePawn(Pawn pawn) => pawn.Move(DiceNumber);
    }
}
