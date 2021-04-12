using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;

namespace LudoConsole.Main
{
    public class GamePlay
    {
        public (Action<GamePlay> Init, Action OnAfterMove) SaveActions { get; set; }
        private IDice dice { get; set; }
        private Func<bool> RunCondition { get; set; }
        public GamePlay(List<IGamePlayer> players, IDice dice, Func<bool> runCondition, IGamePlayer first = null)
        {
            this.dice = dice;
            RunCondition = runCondition;
            Players = players;
            OrderOfTeams = OrderOfTeams.Intersect(players.Select(x => x.Color)).ToList();
            if (first != null) SetFirstTeam(first.Color);
        }
        public void Start()
        {
            SaveActions.Init?.Invoke(this);
            while (RunCondition())
            {
                CurrentPlayer().Play(dice);
                SaveActions.OnAfterMove?.Invoke();
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