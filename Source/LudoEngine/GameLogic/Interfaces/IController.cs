using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.GameLogic.Interfaces
{
    public interface IController
    {
        public List<Pawn> Select(List<Pawn> pawns, bool takeTwo);
    }
}