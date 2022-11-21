using LudoEngine.GameLogic.GamePlayers;

namespace LudoEngine.GameLogic.Interfaces
{
    public interface IInfoDisplay
    {
        void Update(string newString);
        public void UpdateDiceRoll(IGamePlayer player, int result);
        public void UpdateDiceRoll(Stephan stephan, int result);
    }
}
