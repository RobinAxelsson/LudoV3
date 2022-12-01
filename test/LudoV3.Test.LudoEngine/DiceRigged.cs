using LudoEngine.Interfaces;

namespace LudoTest
{
    public class DiceRigged : IDice
    {
        private int[] ResultSeries { get; }
        private int Index { get; set; }
        public DiceRigged(int[] resultSeries)
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
