using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoLudoGame(IEnumerable<DtoGameSquare> GameSquares);
}