using LudoConsole.UI.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.BoardUnits.Main;

namespace LudoConsole.UI.Controls
{
    public class BoardRenderer
    {
        private readonly IEnumerable<ISquareDrawable> _squareDrawables;
        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }
        public BoardRenderer(List<IGameSquare> gameSquares)
        {
            _squareDrawables = DrawBoardConvert(gameSquares);
            Pawn.GameOverEvent += OnGameOver;
            _thread = new Thread((() =>
            {
                while (IsRunning)
                {
                    ConsoleWriter.UpdateBoard(_squareDrawables.ToList());
                    Thread.Sleep(200);
                }
            }));
        }

        public static BoardRenderer StartRender(List<IGameSquare> gameSquares)
        {
            var boardRenderer = new BoardRenderer(gameSquares);
            boardRenderer.Start();
            return boardRenderer;
        }

        public void Start()
        {
            UiColorConfiguration.SetDefault();
            IsRunning = true;
            _thread.Start();
            InfoDisplay.Init();
        }
        public void OnGameOver()
        {
            Console.ReadKey();
            IsRunning = false;
            _thread.Join();
            Console.ReadKey();
        }

        private IEnumerable<ISquareDrawable> DrawBoardConvert(List<IGameSquare> squares)
        {
            var squareDrawables = GetNonBaseSquareDrawables(squares).ToArray();

            var x = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.Y);

            var baseDraws = squares
                .Where(x => x.GetType() == typeof(BaseSquare))
                .Select(square => new BaseDrawable(square, (x, y)))
                .Select(x => (ISquareDrawable)x);

            return squareDrawables.Select(x => (ISquareDrawable)x).Concat(baseDraws).ToList();
        }

        private static IEnumerable<SquareDrawable> GetNonBaseSquareDrawables(List<IGameSquare> squares)
        {
            var squareDraws =
                squares.Where(x => x.GetType() != typeof(BaseSquare)).Select(x => new SquareDrawable(x));
            return squareDraws;
        }
    }
}
