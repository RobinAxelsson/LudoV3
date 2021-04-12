using LudoConsole.Main;
using LudoConsole.UI.Interfaces;
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
        public WriterThread(GamePlay gamePlay, IEnumerable<ISquareDrawable> squareDrawables)
        {
            gamePlay.GameOverEvent += OnGameOver;
            _squareDrawables = squareDrawables;
            _thread = new Thread(new ThreadStart(() =>
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
        public void OnGameOver(object source, EventArgs e)
        {
            IsRunning = false;
            _thread.Join();
            ConsoleWriter.ClearScreen();
            Console.WriteLine("Game Over"); //testmethod
            Console.ReadKey();
        }
    }
}
