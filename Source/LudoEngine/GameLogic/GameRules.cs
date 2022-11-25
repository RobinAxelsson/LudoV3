using LudoEngine.BoardUnits.Main;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine.GameLogic
{
    public static class GameRules
    {
        public static List<Pawn> SelectablePawns(TeamColorCore color, int dieRoll)
        {
            var pawnsInBase = BoardPawnFinder.PawnsInBase(StaticBoard.BoardSquares, color);
            var activeSquares = BoardNavigation.PawnBoardSquares(StaticBoard.BoardSquares, color);

            if (dieRoll == 1 || dieRoll == 6)
                return activeSquares.SelectMany(x => x.Pawns).Concat(pawnsInBase).ToList();
            else
                return activeSquares.SelectMany(x => x.Pawns).ToList(); 
        }
        public static void SaveFirstTime(TeamColorCore currentTurn) => DatabaseManagement.SaveAndGetGame(currentTurn);
        public static bool CanTakeOutTwo(TeamColorCore color, int diceRoll) => BoardPawnFinder.PawnsInBase(StaticBoard.BoardSquares, color).Count > 1 && diceRoll == 6;
    }
}
