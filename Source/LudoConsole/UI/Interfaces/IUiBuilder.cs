using LudoConsole.Main;
using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoConsole.UI.Interfaces
{
    public interface IUiBuilderSetColors
    {
        public IUiBuilderDrawBoardConvert ColorSettings(Action setColor);
    }
    public interface IUiBuilderDrawBoardConvert
    {
        public IUiBuilderStopEvent DrawBoardConvert(List<IGameSquare> squares);
    }
    public interface IUiBuilderStopEvent
    {
        public UiBuilderToWriterThread StopEventFrom(GamePlay gamePlay);
    }
    public interface IUiBuilderLoopCondition
    {
        public UiBuilderToWriterThread LoopCondition(Func<bool> loopCondition);
    }
    public interface UiBuilderToWriterThread
    {
        public WriterThread ToWriterThread();
    }
}
