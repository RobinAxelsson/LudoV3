using LudoConsole.Main;
using LudoConsole.UI.Interfaces;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.BoardUnits.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LudoConsole.UI.Controls
{
    public class UiThreadBuilder :
        IUiBuilderSetColors,
        IUiBuilderDrawBoardConvert,
        UiBuilderToWriterThread,
        IUiBuilderStopEvent

    {
        public static IUiBuilderSetColors StartBuild() => new UiThreadBuilder();
        private IEnumerable<ISquareDrawable> _squareDrawables { get; set; }
        private GamePlay _gamePlay { get; set; }
        public IUiBuilderDrawBoardConvert ColorSettings(Action setColor)
        {
            setColor();
            return this;
        }
        public IUiBuilderStopEvent DrawBoardConvert(List<IGameSquare> squares)
        {
            var squareDraws = squares.Where(x => x.GetType() != typeof(BaseSquare)).Select(x => new SquareDrawable(x));
            var x = squareDraws.Select(x => x.MaxCoord()).Max(x => x.X);
            var y = squareDraws.Select(x => x.MaxCoord()).Max(x => x.Y);
            var baseDraws = squares.Where(x => x.GetType() == typeof(BaseSquare))
            .Select(square => new BaseDrawable(square, (x, y)))
            .Select(x => (ISquareDrawable)x);
            _squareDrawables = squareDraws.Select(x => (ISquareDrawable)x).Concat(baseDraws).ToList();

            return this;
        }
        public UiBuilderToWriterThread StopEventFrom(GamePlay gamePlay)
        {
            _gamePlay = gamePlay;
            return this;
        }
        public WriterThread ToWriterThread() => new WriterThread(_gamePlay, _squareDrawables);
    }
}
