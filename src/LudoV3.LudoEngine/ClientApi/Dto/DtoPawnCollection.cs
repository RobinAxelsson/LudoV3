﻿using System.Collections.Generic;

namespace LudoEngine.ClientApi.Dto
{
    public record DtoPawnCollection(IEnumerable<DtoPawn> AllPlayingPawns);
}