using System.Collections.Generic;
using LudoEngine.ClientApi.Dto;

namespace LudoEngine.ClientApi
{
    public abstract class LudoClientBase
    {
        public abstract void OnNewGame(DtoGame dtoGame);
        public abstract void OnReceiveOtherMove();
        public abstract DtoPawn OnMakeMove(DtoMakeMove dtoMakeMove);


    }

    //public enum TeamColor
}

