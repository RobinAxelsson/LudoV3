using System;
using System.Collections.Generic;
using System.Linq;
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
            OrderOfTeams = OrderOfTeams.Intersect(players.Select(x => x.Color)).ToList();
            if (first != null) SetFirstTeam(first.Color);
        }

        public static event Action<GamePlay> GameStartEvent;
        public static event Action<IGamePlayer> OnPlayerEndsRoundEvent;
        public void Start()
        {
            GameStartEvent?.Invoke(this);
            while (true)
            {
                CurrentPlayer().Play(dice);
                var newRoundPlayer = NextPlayer();
                OnPlayerEndsRoundEvent?.Invoke(newRoundPlayer);
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
        public IGamePlayer NextPlayer()
        {
            iCurrentTeam++;
            iCurrentTeam = iCurrentTeam >= Players.Count ? 0 : iCurrentTeam;
            return CurrentPlayer();
        }
        public IGamePlayer CurrentPlayer() => Players.Find(x => x.Color == OrderOfTeams[iCurrentTeam]);

    }
}