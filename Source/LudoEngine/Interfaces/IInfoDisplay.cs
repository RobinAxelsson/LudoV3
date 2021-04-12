using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.GameLogic.Interfaces
{
    public interface IInfoDisplay
    {
        void Update(string newString);
        public void UpdateDiceRoll(TeamColor color, int diceRoll, Action throwStyle);
    }
}
