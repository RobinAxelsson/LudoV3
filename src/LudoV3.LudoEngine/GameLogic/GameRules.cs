using LudoEngine.DbModel;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.Board;
using LudoEngine.Enums;

namespace LudoEngine.GameLogic
{
    internal static class GameRules
    {
        public static List<Pawn> SelectablePawns(TeamColor color, int dieRoll)
        {
            var pawnsInBase = GameBoard.PawnsInBase(GameBoard.BoardSquares, color);
            var activeSquares = GameBoard.PawnBoardSquares(GameBoard.BoardSquares, color);

            if (dieRoll == 1 || dieRoll == 6)
                return activeSquares.SelectMany(x => x.Pawns).Concat(pawnsInBase).ToList();
            else
                return activeSquares.SelectMany(x => x.Pawns).ToList(); 
        }
        public static void SaveFirstTime(TeamColor currentTurn) => DatabaseManagement.SaveAndGetGame(currentTurn);
        public static bool CanTakeOutTwo(TeamColor color, int diceRoll) => GameBoard.PawnsInBase(GameBoard.BoardSquares, color).Count > 1 && diceRoll == 6;
    }
}
