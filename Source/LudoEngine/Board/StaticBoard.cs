using System.Collections.Generic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board
{
    public static class StaticBoard
    {
        public static List<IGameSquare> BoardSquares { get; set; }

        private const string _filePath = @"Board/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            BoardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }
    }
}
