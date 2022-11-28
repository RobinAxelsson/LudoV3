using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoGame(IEnumerable<DtoGameSquare> GameSquares, IEnumerable<DtoPlayer> players);
}