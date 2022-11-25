using LudoEngine.BoardUnits.Interfaces;
using System.Collections.Generic;


namespace LudoEngine.BoardUnits.Main
{
    public static class StaticBoard
    {
        public static List<IGameSquare> BoardSquares { get; set; }

        private const string _filePath = @"LudoORM/Map/BoardMap.txt";
        public static void Init(string filePath = _filePath)
        {
            BoardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }
    }

    public class Board
    {
        public List<IGameSquare> BoardSquares { get; set; }

        private const string _filePath = @"LudoORM/Map/BoardMap.txt";

        public Board(string filePath = _filePath)
        {
            BoardSquares = GameSquareFactory.CreateGameSquares(filePath);
        }
    }
}
