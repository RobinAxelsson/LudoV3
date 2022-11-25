using System.Collections.Generic;
using LudoEngine.Enum;

namespace LudoEngine.ClientApi
{
    public record DtoMakeMove(int DiceRoll, IEnumerable<DtoPawn> moveablePawns, TeamColorCore ColorCore);
}