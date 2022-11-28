﻿using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.Board;

namespace LudoEngine.GameLogic
{
    public static class GameRules
    {
        public static List<Pawn> SelectablePawns(TeamColor color, int dieRoll)
        {
            var pawnsInBase = BoardPawnFinder.PawnsInBase(GameBoard.BoardSquares, color);
            var activeSquares = BoardNavigation.PawnBoardSquares(GameBoard.BoardSquares, color);

            if (dieRoll == 1 || dieRoll == 6)
                return activeSquares.SelectMany(x => x.Pawns).Concat(pawnsInBase).ToList();
            else
                return activeSquares.SelectMany(x => x.Pawns).ToList(); 
        }
        public static void SaveFirstTime(TeamColor currentTurn) => DatabaseManagement.SaveAndGetGame(currentTurn);
        public static bool CanTakeOutTwo(TeamColor color, int diceRoll) => BoardPawnFinder.PawnsInBase(GameBoard.BoardSquares, color).Count > 1 && diceRoll == 6;
    }
}
