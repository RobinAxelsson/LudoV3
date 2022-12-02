using LudoEngine.ClientApi.Dto;

namespace LudoEngine.ClientApi
{
    public abstract class LudoClientBase
    {
        public abstract void OnNewGame(DtoGameBoard dtoLudoGame);
        public abstract void OnUpdate(DtoPawnCollection allPawns);
    }

    //public enum TeamColor
}

