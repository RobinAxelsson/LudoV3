using LudoConsole.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.Dice;
using LudoEngine.GameLogic.GamePlayers;
using LudoEngine.GameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace LudoEngine.Creation
{
    public class GamePlayBuilder
    {
        public GamePlayBuilder(IInfoDisplay display, IController controller)
        {
            display = display;
            Controller = controller;
        }
        private List<TeamColor> TeamColors { get; set; } = new();
        private IInfoDisplay display { get; set; }
        private List<IGamePlayer> gamePlayers { get; set; }
        private IGamePlayer first { get; set; }
        private IController Controller { get; set; }
        private IDice dice { get; set; }
        private void AddColor(TeamColor color)
        {
            if (TeamColors.Contains(color)) throw new Exception("There can only be one player per color");
            TeamColors.Add(color);
        }
        public GamePlayBuilder AddHuman(TeamColor color)
        {
            AddColor(color);
            gamePlayers.Add(new HumanPlayer(color, display.UpdateDiceRoll, Controller));
            return this;
        }
        public GamePlayBuilder AddAI(TeamColor color, bool log = false)
        {
            AddColor(color);

            if (log)
                gamePlayers.Add(new Stephan(color, display.UpdateDiceRoll, new StefanLog(color)));
            else
                gamePlayers.Add(new Stephan(color, display.UpdateDiceRoll));
            
            return this;
        }
        public GamePlayBuilder AddStandardDice()
        {
            dice = new Dice(1, 6);
            return this;
        }
        public GamePlayBuilder FirstColor(TeamColor color)
        {
            first = gamePlayers.Find(x => x.Color == color);
            return this;
        }
        public GamePlay Play()
        {
            if (dice == null) throw new Exception("You need a dice");
            return new GamePlay(gamePlayers, dice, first);
        }
    }
}
