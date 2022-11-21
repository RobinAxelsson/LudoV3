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
        public UiBuilderToWriterThread DrawBoardConvert(List<IGameSquare> squares);
    }
    public interface UiBuilderToWriterThread
    {
        public BoardRenderer ToWriterThread();
    }
}
