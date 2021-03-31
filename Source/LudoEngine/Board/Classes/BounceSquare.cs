//using LudoEngine.Board.Intefaces;
//using LudoEngine.Enum;
//using LudoEngine.Models;
//using System.Collections.Generic;
//using System.Linq;

//namespace LudoEngine.Board.Classes
//{
//    public class BounceSquare : IGameSquare
//    {
//        public BounceSquare(int boardX, int boardY)
//        {
//            BoardX = boardX;
//            BoardY = boardY;
//        }
//        public int BoardX { get; set; }
//        public int BoardY { get; set; }
//        public List<Pawn> Pawns { get; set; } = new List<Pawn>();

//        private List<(Pawn pawn, BoardDirection direction)> pawnDirections = new List<(Pawn, BoardDirection)>();
//        public BoardDirection DefaultDirection { get; set; }
//        public BoardDirection DirectionNext(Pawn pawn)
//        {
//            if(pawnDirections.Select(x => x.pawn).Contains(pawn) == false)
//            {
//                pawnDirections.Add((pawn, DefaultDirection));
//                return DefaultDirection;
//            }
//            else
//            {
//                var pawnDir = pawnDirections.Find(x => x.pawn == pawn);
//                pawnDir.direction = BoardUtils.FlipDirection(pawnDir.direction);
//                return pawnDir.direction;
//            }
//        }
//    }
//}