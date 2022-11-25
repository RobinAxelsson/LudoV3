using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoConsole.Controller;
using LudoConsole.Ui.Components;
using LudoEngine.Models;

namespace LudoConsole.Ui
{
    public class BoardRenderer
    {
        private readonly IEnumerable<BoardSquareBase> _squareDrawables;

        public BoardRenderer(IEnumerable<ConsoleGameSquare> gameSquares)
        {
            _squareDrawables = LudoSquareFactory.CreateBoardSquares(gameSquares);
            Pawn.GameOverEvent += OnGameOver;
            _thread = new Thread(() =>
            {
                while (IsRunning)
                {
                    ConsoleWriter.UpdateBoard(_squareDrawables.ToList());
                    Thread.Sleep(200);
                }
            });
        }

        private Thread _thread { get; }
        private bool IsRunning { get; set; }

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