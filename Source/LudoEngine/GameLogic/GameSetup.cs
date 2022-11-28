using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoEngine.Board;
using LudoEngine.Board.Square;
using LudoEngine.Interfaces;

namespace LudoEngine.GameLogic
{
    public static class GameSetup
    { 
        public static void LoadSavedPawns(List<PawnSavePoint> savePoints)
        {
            foreach (var sp in savePoints)
            {
                var squareToPut = GameBoard.BoardSquares.Find(bs => sp.XPosition == bs.BoardX && sp.YPosition == bs.BoardY);

                squareToPut.Pawns.Add(new Pawn(sp.Color));
            }
        }
        public static List<TeamColor> Load(List<IGameSquare> gameSquares, List<(TeamColor color, (int X, int Y) position)> teamCoords)
        {
            foreach (var teamCoord in teamCoords)
                gameSquares.Find(x => x.BoardX == teamCoord.position.X && x.BoardY == teamCoord.position.Y).Pawns.Add(new Pawn(teamCoord.color));

            return teamCoords.Select(x => x.color).Distinct().ToList();;
        }
        public static void SetUpPawnsNewGame(List<IGameSquare> gameSquares, TeamColor[] colors = null)
        {
            colors ??= new [] { TeamColor.Blue, TeamColor.Red, TeamColor.Green, TeamColor.Yellow };

            //var teamCoords = new List<(TeamColor color, (int X, int Y) position)>();
            int pawnsCount = colors == null ? 16 : 4 * colors.Count();
            
            List<SquareTeamBase> bases = gameSquares.FindAll(x => x.GetType() == typeof(SquareTeamBase)).Select(x => (SquareTeamBase)x).ToList();

            int iTeam = 0;
            for (int i = 1; i <= pawnsCount; i++)
            {
                var teamColor = colors[iTeam];
                bases.Find(x => x.Color == teamColor).Pawns.Add(new Pawn(teamColor));
                if (i % 4 == 0) iTeam++;
            }
        }
    }
}
