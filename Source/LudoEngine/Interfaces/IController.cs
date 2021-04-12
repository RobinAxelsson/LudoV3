using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.GameLogic.Interfaces
{
    public interface IController
    {
        public void Throw();
        public List<Pawn> Select(List<Pawn> pawns, bool takeTwo);
    }
}