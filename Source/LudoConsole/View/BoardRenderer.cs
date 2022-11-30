using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoConsole.View.Components;
using LudoEngine.GameLogic;

namespace LudoConsole.View
{
    internal class BoardRenderer
    {
        private readonly IReadOnlyList<ViewGameSquareBase> _uiGameSquares;

        public BoardRenderer(IReadOnlyList<ViewGameSquareBase> uiGameSquares)
        {
            _uiGameSquares = uiGameSquares;
            Pawn.GameOverEvent += OnGameOver;
        }

        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }

        public static BoardRenderer StartRender(IReadOnlyList<ViewGameSquareBase> uiGameSquares)
        {
            var boardRenderer = new BoardRenderer(uiGameSquares);
            boardRenderer.Start();
            return boardRenderer;
        }

        public void Start()
        {
            ColorManager.SetDefault();
            IsRunning = true;

            _thread = new Thread(() =>
            {
                while (IsRunning)
                {
                    ConsoleWriter.UpdateBoard(_uiGameSquares.ToList());
                    Thread.Sleep(200);
                }
            });

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