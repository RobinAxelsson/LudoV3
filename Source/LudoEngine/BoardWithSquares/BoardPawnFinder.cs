using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoEngine.BoardUnits.Main
{
    public static class BoardPawnFinder
    {
        public static List<Pawn> PawnsInBase(List<IGameSquare> boardSquares, TeamColor color) =>
            boardSquares.FirstOrDefault(x => x.GetType() == typeof(SquareTeamBase) && x.Color == color)?.Pawns ?? Array.Empty<Pawn>().ToList();

        public static IGameSquare FindPawnSquare(List<IGameSquare> boardSquares, Pawn pawn) => boardSquares.Find(x => x.Pawns.Contains(pawn));
        public static List<Pawn> AllBaseAndPlayingPawns(List<IGameSquare> boardSquares) => boardSquares.SelectMany(x => x.Pawns).ToList();
        public static List<Pawn> OutOfBasePawns(List<IGameSquare> boardSquares, TeamColor color) => AllPlayingPawns(boardSquares).FindAll(x => x.Color == color);
        public static List<Pawn> AllPlayingPawns(List<IGameSquare> boardSquares) => boardSquares.FindAll(x => x.GetType() != typeof(SquareTeamBase) && x.GetType() != typeof(SquareGoal)).SelectMany(x => x.Pawns).ToList();

        public static List<Pawn> GetTeamPawns(List<IGameSquare> boardSquares, TeamColor color) => boardSquares.SelectMany(x => x.Pawns).Where(x => x.Color == color).ToList();
    }
}