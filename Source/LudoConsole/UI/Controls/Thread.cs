using LudoConsole.UI.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoConsole.UI.Models;

namespace LudoConsole.UI.Controls
{
    public class BoardRenderer
    {
        private readonly IEnumerable<ISquareDrawable> _squareDrawables;
        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }
        public BoardRenderer(IEnumerable<ConsoleGameSquare> gameSquares)
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

        public static BoardRenderer StartRender(IEnumerable<ConsoleGameSquare> gameSquares)
        {
            var boardRenderer = new BoardRenderer(gameSquares);
            boardRenderer.Start();
            return boardRenderer;
        }

        public void Start()
        {
            UiColor.SetDefault();
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

        private IEnumerable<ISquareDrawable> DrawBoardConvert(IEnumerable<ConsoleGameSquare> squares)
        {
            var squareDraws = squares.Where(x => !x.IsBase).Select(x => new SquareDrawable(x));
            
            var squareDrawables = squareDraws.ToArray();

            var x = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = squareDrawables.Select(x => x.MaxCoord()).Max(x => x.Y);

            var baseDraws = squares
                .Where(x => x.IsBase)
                .Select(square => new BaseDrawable(square, (x, y)))
                .Select(x => (ISquareDrawable)x);

            return squareDrawables.Select(x => (ISquareDrawable)x).Concat(baseDraws).ToList();
        }
    }
}
