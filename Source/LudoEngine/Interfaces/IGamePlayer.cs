using LudoEngine.Enum;

namespace LudoEngine.GameLogic.Interfaces
{ 
    public interface IGamePlayer
    {
        public TeamColor Color { get; set; }
        public void Play(IDice dice);
    }
}