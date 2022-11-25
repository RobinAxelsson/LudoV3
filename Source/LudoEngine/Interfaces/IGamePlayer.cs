using LudoEngine.Enum;

namespace LudoEngine.GameLogic.Interfaces
{ 
    public interface IGamePlayer
    {
        public TeamColorCore Color { get; set; }
        public void Play(IDice dice);
    }
}