using System;
using System.Collections.Generic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board
{
    public static class GameBoard
    {
        private static List<IGameSquare> _boardSquares;
        public static List<IGameSquare> BoardSquares => _boardSquares ?? throw new ArgumentNullException(nameof(BoardSquares));

        private const string _filePath = @"Board/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            _boardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }
    }
}
