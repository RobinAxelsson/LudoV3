using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.BoardUnits.Main
{
    public static class GameSetup
    { 
        public static void Load(List<IGameSquare> gameSquares, List<(TeamColor color, (int X, int Y) position)> teamCoords)
        {
            foreach (var teamCoord in teamCoords)
            {
                gameSquares.Find(x => x.BoardX == teamCoord.position.X && x.BoardY == teamCoord.position.Y).Pawns.Add(new Pawn(teamCoord.color));
            }
        }
        public static void NewGame(List<IGameSquare> gameSquares, int players)
        {
            var teamCoords = new List<(TeamColor color, (int X, int Y) position)>();
            int pawnsCount = 4 * players;
            List<BaseSquare> bases = gameSquares.FindAll(x => x.GetType() == typeof(BaseSquare)).Select(x => (BaseSquare)x).ToList();

            int iTeam = 0;
            for (int i = 1; i <= pawnsCount; i++)
            {
                var teamColor = (TeamColor)iTeam;
                bases.Find(x => x.Color == teamColor).Pawns.Add(new Pawn(teamColor));
                if (i % 4 == 0) iTeam++;
            }
        }
    }
}
