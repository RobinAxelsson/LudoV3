using System;
using LudoEngine.Interfaces;

namespace LudoEngine.GameLogic
{
    internal sealed class Dice : IDice
    {
        private int Highest { get; set; }
        private int Lowest { get; set; }
        private Random random { get; set; }
        public Dice(int lowest, int highest)
        {
            Highest = highest + 1;
            Lowest = lowest;
            random = new Random();
        }
        public int Roll() => random.Next(Lowest, Highest);
    }
}