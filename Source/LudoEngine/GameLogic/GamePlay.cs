using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.Board;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.Exceptions;
using LudoEngine.Interfaces;

namespace LudoEngine.GameLogic
{
    public class GamePlay
    {
        private IDice dice { get; set; }
        private int iCurrentTeam { get; set; }

        private List<TeamColor> OrderOfTeams = new()
        {
            TeamColor.Blue,
            TeamColor.Red,
            TeamColor.Green,
            TeamColor.Yellow
        };

        public static event Action<GamePlay> GameStartEvent;
        public static event Action<GamePlay> OnPlayerEndsRoundEvent;
        public List<IGamePlayer> Players { get; set; }
        private readonly List<IGamePlayer> _winners = new();

        public GamePlay(IEnumerable<IGamePlayer> players, IDice dice, IGamePlayer first = null)
        {
            this.dice = dice;
            Players = players.ToList();
            OrderOfTeams = OrderOfTeams.Intersect(Players.Select(x => x.Color)).ToList();
            if (first != null) SetFirstTeam(first.Color);
        }


        public void Start()
        {
            GameStartEvent?.Invoke(this);
            while (true)
            {
                CurrentPlayer().Play(dice);
                GameBoard.AllPlayingPawns(GameBoard.BoardSquares).ForEach(x => x.IsSelected = false);
                OnPlayerEndsRoundEvent?.Invoke(this);
                CheckForWinner();
                NextPlayer();
            }
        }

        public void SetFirstTeam(TeamColor color) => iCurrentTeam = OrderOfTeams.FindIndex(x => x == color);

        private void CheckForWinner()
        {
            var pawns = GameBoard.AllBaseAndPlayingPawns(GameBoard.BoardSquares)
                .Where(x => x.Color == OrderOfTeams[iCurrentTeam]).ToList();

            if (!pawns.Any())
            {
                var player = CurrentPlayer();
                _winners.Add(player);
                Players.Remove(player);
                OrderOfTeams.RemoveAt(iCurrentTeam);

                if (!Players.Any())
                    throw new NoPlayersException("All players has scored all pawns.");
            }
        }

        public void NextPlayer()
        {
            StageSaving.CurrentTeam = iCurrentTeam;
            iCurrentTeam++;
            iCurrentTeam = iCurrentTeam >= Players.Count ? 0 : iCurrentTeam;
        }
        public TeamColor NextPlayerForSave()
        {
            var i = iCurrentTeam + 1;
            i = i >= Players.Count ? 0 : i;
            return OrderOfTeams[i];
        }

        public IGamePlayer CurrentPlayer()
        {
            return Players.Find(x => x.Color == OrderOfTeams[iCurrentTeam]);
        }

        public IGamePlayer CurrentPlayer(bool stageSaving) => Players.Find(x => x.Color == OrderOfTeams[StageSaving.CurrentTeam]);


    }
}