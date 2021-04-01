using LudoEngine.BoardUnits.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Interfaces
{
    public interface IDrawableSquare
    {
        public IGameSquare Square { get; set; }
        public List<IDrawable> UpdatePawns();
        public List<IDrawable> Memory { get; set; }

    }
}
