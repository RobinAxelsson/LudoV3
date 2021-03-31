using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic
{
    public class Dice
    {
        public static int RollDice()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 6);

            return number;
        }
    }
}
