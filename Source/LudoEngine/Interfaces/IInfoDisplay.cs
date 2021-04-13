using LudoConsole.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.GamePlayers;
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
        public void UpdateDiceRoll(IGamePlayer player, int result);
        public void UpdateDiceRoll(Stephan stephan, int result);
    }
}
