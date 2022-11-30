using LudoEngine.Enums;

namespace LudoEngine.Interfaces
{ 
    public interface IGamePlayer
    {
        public TeamColor Color { get; set; }
        public void Play(IDice dice);
    }
}