using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Interfaces
{
    public interface IShape
    {
        public (int X, int Y) PointA { get; set; }
        public (int X, int Y) PointB { get; set; }
        public List<(int X, int Y)> GetShape();
    }
}
