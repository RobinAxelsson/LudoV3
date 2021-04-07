using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine.GameLogic
{
    public static class GameRules
    {
        private static int amountOfSixesRolled = 1;

        public static List<Pawn> SelectablePawns(TeamColor color, int dieRoll)
        {
            var pawnsInBase = Board.PawnsInBase(color);
            var activeSquares = Board.PawnBoardSquares(color);

            if (dieRoll == 1 || dieRoll == 6)
                return activeSquares.SelectMany(x => x.Pawns).Concat(pawnsInBase).ToList();
            else
                return activeSquares.SelectMany(x => x.Pawns).ToList(); 
        }
        public static void LeaveHome(int dieRoll)
        {
            if (dieRoll == 1)
            {
                Menu.ShowMenu("Choose pawn to move", new string[] { "Pawn 1", "Pawn 2", "Pawn 3", "Pawn 4" });
            }
            else if (dieRoll == 6)
            {
                int choice = Menu.ShowMenu("What do you want to do", new string[] { "Put two pieces on board", "Move your pawn 6 steps" });
                if (choice == 0)
                {
                    // puts two pieces onto board
                }
                if (choice == 1)
                {
                    // mvoes your chosen pawn 6 steps forward
                }
            }
        }
        /*
        public static void MovePawn(int dieRoll, Pawn pawn)
        {

            bool encounteredOtherPawn = true;

            if (encounteredOtherPawn)
            {

            }
            else
            {
                // Gör logik för att flytta en pawn
            }

        }
        */
        public static void KnockedHomePawn(Pawn knocker, Pawn knockedOut)
        {
            // Move knockedOut to Homebase
        }
        public static bool RollDieAgain(int dieRoll)
        {

            if (dieRoll == 6)
            {
                amountOfSixesRolled++;
                return true;
            }
            else
            {
                amountOfSixesRolled = 1;
                return false;
            }
        }
        public static void PlayerHasWon()
        {
            // If all pawns are in goal, player won.

        }
        public static bool CheckCanMoveToGoal(int dieRoll, Pawn pawn, int xGoal, int yGoal)
        {
            //Checks to see how many steps there are between the pawn and the goal
            int stepsToGoal = 0;
            if (dieRoll == stepsToGoal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }























        /*
         * -Pseudo-
         * 
         * If 4 pieces are home
         *  roll die
         *      if die roll is 1
                   add one piece to board
                if die roll is 6
                    add two pieces to board


            if < 4 pieces are home
             roll die
                if die roll is 1
           
         * 
        */
    }
}
