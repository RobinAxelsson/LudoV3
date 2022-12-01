using System.Collections.Generic;
using LudoEngine.ClientApi.Enums;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoGameSquare(int BoardX, int BoardY, LudoColor Color, SquareType SquareType, IEnumerable<DtoPawn> Pawns);
}