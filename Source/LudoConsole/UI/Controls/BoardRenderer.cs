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
            //_squareDrawables = DrawBoardConvert(gameSquares);
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

        //private IEnumerable<DrawableSquareBase> DrawBoardConvert(IEnumerable<ConsoleGameSquare> squares)
        //{
        //    var squareDraws = squares.Where(x => !x.IsBase)
        //        .Select(x => new DrawableSquare(x))
        //        .OfType<DrawableSquareBase>().ToArray();
            

        //    var (x, y) = GetBoardMaxPoint(squareDraws);

        //    var baseDraws = squares
        //        .Where(x => x.IsBase)
        //        .Select(square => new DrawableTeamBase(square, (x, y)));

        //    return squareDraws.Concat(baseDraws).ToList();
        //}

        //private static (int x, int y) GetBoardMaxPoint(DrawableSquareBase[] drawableSquares)
        //{
        //    var x = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.X);
        //    var y = drawableSquares.Select(x => x.MaxCoord()).Max(x => x.Y);
        //    return (x,y);
        //}
    }
}
