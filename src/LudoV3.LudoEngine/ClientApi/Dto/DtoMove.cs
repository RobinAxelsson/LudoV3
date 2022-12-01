using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoMove(DtoPlayer Player, IEnumerable<DtoPawn> MovedPawns);
}