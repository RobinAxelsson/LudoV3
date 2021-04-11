using LudoConsole.UI.Controls;
using LudoEngine.BoardUnits.Intefaces;
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
        public IUiBuilderDrawaBoardConvert ColorSettings(Action setColor);
    }
    public interface IUiBuilderDrawaBoardConvert
    {
        public IUiBuilderLoopCondition DrawBoardConvert(List<IGameSquare> squares);
    }
    public interface IUiBuilderLoopCondition
    {
        public UiBuilderToWriterThread LoopCondition(Func<bool> loopCondition);
    }
    public interface UiBuilderToWriterThread
    {
        public Thread ToWriterThread();
    }
}
