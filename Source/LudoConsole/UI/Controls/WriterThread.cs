using LudoConsole.UI.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LudoConsole.UI.Controls
{
    public class WriterThread
    {
        private IEnumerable<ISquareDrawable> _squareDrawables { get; set; }
        private Thread _thread { get; set; }
        private bool IsRunning { get; set; }
        public WriterThread(IEnumerable<ISquareDrawable> squareDrawables)
        {
            _squareDrawables = squareDrawables;
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
        public void Start()
        {
            IsRunning = true;
            _thread.Start();
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
