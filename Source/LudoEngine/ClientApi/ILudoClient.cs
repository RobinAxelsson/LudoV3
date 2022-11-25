using System.Collections.Generic;

namespace LudoEngine.ClientApi
{
    public interface ILudoClient
    {
        //gameSquares Dtos
        public void OnNewGame(DtoGame dtoGame);

        //pawnDtos
        public void OnReceiveUpdatedPawns(IEnumerable<DtoPawn> pawnDtos);

        //pawnDto
        public DtoPawn OnMakeMove(DtoMakeMove dtoMakeMove);


    }

    //public enum TeamColor
}

