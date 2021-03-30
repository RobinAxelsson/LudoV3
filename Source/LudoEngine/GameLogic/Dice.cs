using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic
{
    class Dice
    {
        public int RollDice()
        {
            int Number = Convert.ToInt16(new Random(6));

            return Number;
        }
    }
}
