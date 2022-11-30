using System.Collections.Generic;
using LudoEngine.Enums;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoMakeMove(int DiceRoll, IEnumerable<DtoPawn> MovablePawns, TeamColor Color);
}