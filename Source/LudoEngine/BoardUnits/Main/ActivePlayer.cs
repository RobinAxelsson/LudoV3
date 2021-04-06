using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.BoardUnits.Main
{
    public static class ActivePlayer
    {
        private static List<TeamColor> OrderOfTeams = new List<TeamColor>
        {
            TeamColor.Blue,
            TeamColor.Green,
            TeamColor.Red,
            TeamColor.Yellow
        };
        public static TeamColor CurrentTeam() => OrderOfTeams[iCurrentTeam];
        private static int iCurrentTeam { get; set; }
        private static Pawn SelectedPawn { get; set; }
        private static int DiceNumber { get; set; }
        public static void FirstTeam(TeamColor color) => iCurrentTeam = OrderOfTeams.FindIndex(x => x == color);
        public static void NextTeam()
        {
            iCurrentTeam++;
            if (iCurrentTeam % 4 == 0) iCurrentTeam = 0;
        }
        public static void RollDice() { } //DiceNumber
        public static Pawn SelectPawn(int index)
        {
            var pawns = Board.TeamPawns(CurrentTeam());
            if (pawns.Count <= index) throw new Exception("index cant be higher then amount of pawns");
            if (index < 0) throw new Exception("index cant be lower then zero");
            var pawn = pawns[index];
            return SelectedPawn = pawn;
        }

        public static void MovePawn()
        {
            //pawn move dicenumber. SelectedPawn.Move(DiceNumber)
        }
    }
}
