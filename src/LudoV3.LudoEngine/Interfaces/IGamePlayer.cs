using LudoEngine.Enums;

namespace LudoEngine.Interfaces
{ 
    internal interface IGamePlayer
    {
        public TeamColor Color { get; set; }
        public void Play(IDice dice);
    }
}