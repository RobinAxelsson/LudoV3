using System.Collections.Generic;

namespace LudoEngine.ClientApi
{
    public record DtoGame(IEnumerable<DtoGameSquare> GameSquares);
}