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
        private readonly IEnumerable<DrawableSquareBase> _squareDrawables;
        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }
        public BoardRenderer(IEnumerable<ConsoleGameSquare> gameSquares)
        {
            _squareDrawables = LudoSquareFactory.CreateBoardSquares(gameSquares);
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

    }
}
