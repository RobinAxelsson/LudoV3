using System.Collections.Generic;
using LudoEngine.ClientApi.Dto;

namespace LudoEngine.ClientApi
{
    public abstract class LudoClientBase
    {
        public abstract void OnNewGame(DtoLudoGame dtoLudoGame);
        public abstract void OnGetMove(DtoMove move);
    }

    //public enum TeamColor
}

