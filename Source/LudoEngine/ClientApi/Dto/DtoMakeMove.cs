using System.Collections.Generic;
using LudoEngine.ClientApi.Enums;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoMakeMove(int DiceRoll, IEnumerable<DtoPawn> MovablePawns, LudoColor Color);
}