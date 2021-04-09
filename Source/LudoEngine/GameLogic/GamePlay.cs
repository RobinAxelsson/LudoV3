using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;

namespace LudoConsole.Main
{
    public class GamePlay
    {
        private IDice dice { get; set; }
        public GamePlay(List<IGamePlayer> players, IDice dice, IGamePlayer first = null)
        {
            this.dice = dice;
            Players = players;
            if (first != null) SetFirstTeam(first.Color);
        }
        public void Start()
        {
            while (true)
            {
                CurrentPlayer().Play(dice);
                NextPlayer();
            }
        }
  
        private List<TeamColor> OrderOfTeams = new List<TeamColor>
        {
            TeamColor.Blue,
            TeamColor.Red,
            TeamColor.Green,
            TeamColor.Yellow
        };
        public List<IGamePlayer> Players { get; set; }
        private int iCurrentTeam { get; set; }
        public void SetFirstTeam(TeamColor color) => iCurrentTeam = OrderOfTeams.FindIndex(x => x == color);
        public void NextPlayer()
        {
            iCurrentTeam++;
            iCurrentTeam = iCurrentTeam >= Players.Count ? 0 : iCurrentTeam;
        }
        public TeamColor CachedPlayer()
        {
            int i = iCurrentTeam + 1;
            i = i >= Players.Count ? 0 : i;
            return OrderOfTeams[i];
        }
        public IGamePlayer CurrentPlayer() => Players.Find(x => x.Color == OrderOfTeams[iCurrentTeam]);

    }
}