using LudoConsole.UI.Interfaces;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoConsole.UI.Controls
{
    public class UiBuilder: 
        IUiBuilderSetColors,
      IUiBuilderDrawaBoardConvert,
        IUiBuilderLoopCondition,
        UiBuilderToWriterThread

    {
        public static IUiBuilderSetColors StartBuild() => new UiBuilder();
        private IEnumerable<ISquareDrawable> squareDrawables { get; set; }
        private Func<bool> _loopCondition { get; set; }
        public IUiBuilderDrawaBoardConvert ColorSettings(Action setColor)
        {
            setColor();
            return this;
        }
        public IUiBuilderLoopCondition DrawBoardConvert(List<IGameSquare> squares)
        {
                var squareDraws = squares.Where(x => x.GetType() != typeof(BaseSquare)).Select(x => new SquareDrawable(x));
                var x = squareDraws.Select(x => x.MaxCoord()).Max(x => x.X);
                var y = squareDraws.Select(x => x.MaxCoord()).Max(x => x.Y);
                var baseDraws = squares.Where(x => x.GetType() == typeof(BaseSquare)).Select(square => new BaseDrawable(square, (x, y))).Select(x => (ISquareDrawable)x);
                squareDrawables = squareDraws.Concat(baseDraws).ToList();
            return this;
        }
        public UiBuilderToWriterThread LoopCondition(Func<bool> loopCondition)
        {
            _loopCondition = loopCondition;
            return this;
        }
        public Thread ToWriterThread()
        {
            return new Thread(new ThreadStart(() =>
            {
                while (_loopCondition())
                {
                    ConsoleWriter.UpdateBoard(squareDrawables.ToList());
                    Thread.Sleep(200);
                }
            }));
        }
    }
}
