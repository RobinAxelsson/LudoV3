using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoGameBoard(IEnumerable<DtoGameSquare> GameSquares);
}