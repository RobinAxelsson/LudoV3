using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoConsole.ServerMapping;
using LudoConsole.Ui.Components;
using LudoEngine.GameLogic;

namespace LudoConsole.Ui
{
    public class BoardRenderer
    {
        private readonly IEnumerable<UiGameSquareBase> _squareDrawables;

        public BoardRenderer(IEnumerable<DtoConsoleGameSquare> gameSquares)
        {
            _squareDrawables = UiGameSquareFactory.CreateUiGameSquares(gameSquares);
            Pawn.GameOverEvent += OnGameOver;
        }

        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }

        public static BoardRenderer StartRender(IEnumerable<DtoConsoleGameSquare> gameSquares)
        {
            var boardRenderer = new BoardRenderer(gameSquares);
            boardRenderer.Start();
            return boardRenderer;
        }

        public void Start()
        {
            UiColor.SetDefault();
            IsRunning = true;

            _thread = new Thread(() =>
            {
                while (IsRunning)
                {
                    ConsoleWriter.UpdateBoard(_squareDrawables.ToList());
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