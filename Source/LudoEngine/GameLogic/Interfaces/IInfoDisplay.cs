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
        void UpdateAIDiceRoll(TeamColor color, int diceRoll);
        void UpdateDiceRoll(TeamColor color, int diceRoll);
    }
}
