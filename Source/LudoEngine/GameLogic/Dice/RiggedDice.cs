using LudoEngine.GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic.Dice
{
    public class RiggedDice : IDice
    {
        private int[] ResultSeries { get; set; }
        private int Index { get; set; }
        public RiggedDice(int[] resultSeries)
        {
            ResultSeries = resultSeries;
            Index = 0;
        }
        public int Roll()
        {
            if (Index + 1 > ResultSeries.Length) Index = 0;
            var result = ResultSeries[Index];
            Index++;
            return result;
        }
    }
}
