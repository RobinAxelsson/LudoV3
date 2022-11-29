using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoGameSquare(int BoardX, int BoardY, LudoColor Color, SquareType SquareType, IEnumerable<DtoPawn> Pawns);
}