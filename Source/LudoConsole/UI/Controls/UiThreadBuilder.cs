//using LudoConsole.Main;
//using LudoConsole.UI.Interfaces;
//using LudoConsole.UI.Models;
//using LudoEngine.BoardUnits.Interfaces;
//using LudoEngine.BoardUnits.Main;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;

//namespace LudoConsole.UI.Controls
//{
//    public class UiThreadBuilder

//    {
//        public static UiThreadBuilder StartBuild() => new();
//        private IEnumerable<ISquareDrawable> _squareDrawables { get; set; }
//        public UiThreadBuilder ColorSettings(Action setColor)
//        {
//            setColor();
//            return this;
//        }
//        public UiThreadBuilder DrawBoardConvert(List<IGameSquare> squares)
//        {
//            var squareDrawables = GetNonBaseSquareDrawables(squares).ToArray();

//            var x = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.X);
//            var y = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.Y);
            
//            var baseDraws = squares
//                    .Where(x => x.GetType() == typeof(BaseSquare))
//            .Select(square => new BaseDrawable(square, (x, y)))
//            .Select(x => (ISquareDrawable)x);
            
//            _squareDrawables = squareDrawables.Select(x => (ISquareDrawable)x).Concat(baseDraws).ToList();

//            return this;
//        }

//        private static IEnumerable<SquareDrawable> GetNonBaseSquareDrawables(List<IGameSquare> squares)
//        {
//            var squareDraws =
//                squares.Where(x => x.GetType() != typeof(BaseSquare)).Select(x => new SquareDrawable(x));
//            return squareDraws;
//        }

//        public BoardRender ToWriterThread() => new (_squareDrawables);
//    }
//}
