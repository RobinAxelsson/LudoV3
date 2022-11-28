using System.Collections.Generic;
using LudoEngine.Enum;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoMakeMove(int DiceRoll, IEnumerable<DtoPawn> MovablePawns, TeamColor Color);
}