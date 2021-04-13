using LudoConsole.Main;
using LudoEngine.BoardUnits.Main;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine.Creation
{
    public class GameBuilder :
        IGameBuilderMapBoard,
        IGameBuilderAddDice,
        IGameBuilderSetControl,
        IGameBuilderSetInfoDisplay,
        IGameBuilderLoadOrNew,
        IGameBuilderNewGame,
        IGameBuilderLoadPawns,
        IGameBuilderGamePlay,
        IGameBuilderNewGamePlay,
        IGameBuilderStartingColor,
        IGameBuilderLoadPlayers,
        IGameBuilderSaveConfig
    {
        private IController _control { get; set; }
        private IDice _dice { get; set; }
        private IInfoDisplay _display { get; set; }
        private List<TeamColor> _teamColors { get; set; } = new();
        private TeamColor _first { get; set; }
        private List<IGamePlayer> _gamePlayers { get; set; } = new();
        private Func<bool> _runsWhileCondtition { get; set; }
        private bool _enableSaving { get; set; }
        private void AddColor(TeamColor color)
        {
            if (_teamColors.Contains(color)) throw new Exception("There can only be one player per color");
            _teamColors.Add(color);
        }
        public static IGameBuilderMapBoard StartBuild() => new GameBuilder();
        public IGameBuilderAddDice MapBoard(string filePath)
        {
            Board.BoardSquares = BoardOrm.Map(filePath);
            return this;
        }
        public IGameBuilderSetControl AddDice(IDice dice)
        {
            _dice = dice;
            return this;
        }
        public IGameBuilderSetInfoDisplay SetControl(IController control)
        {
            _control = control;
            return this;
        }
        public IGameBuilderLoadOrNew SetInfoDisplay(IInfoDisplay infoDisplay)
        {
            _display = infoDisplay;
            return this;
        }
        public IGameBuilderLoadPawns LoadGame()
        {
            return this;
        }
        public IGameBuilderLoadPlayers LoadPawns(List<PawnSavePoint> savePoints)
        {
            GameSetup.LoadSavedPawns(savePoints);
            _teamColors = savePoints.Select(x => x.Color).Distinct().ToList();
            //HumanColors = savePoints.Select(x => x.Color && x.PlayerType == 0).Distinct().ToList();
            //AiColors = savePoints.Select(x => x.Color && x.PlayerType == 1).Distinct().ToList();
            return this;
        }
        public IGameBuilderStartingColor LoadPlayers()
        {
            foreach (var color in _teamColors)
            {
                _gamePlayers.Add(new HumanPlayer(color, _control));
            }
            //HumanColors = savePoints.Select(x => x.Color && x.PlayerType == 0).Distinct().ToList();
            //AiColors = savePoints.Select(x => x.Color && x.PlayerType == 1).Distinct().ToList();
            return this;
        }
        public IGameBuilderNewGame NewGame()
        {
            return this;
        }
        public IGameBuilderSaveConfig StartingColor(TeamColor? color)
        {
            if (color != null && _teamColors.Contains((TeamColor)color)) _first = (TeamColor)color;
            else _first = _teamColors[0];
            if (!_teamColors.Contains((TeamColor)color)) throw new Exception("Teamcolor for first player is not present in the game");

            return this;
        }
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color)
        {
            AddColor(color);
            _gamePlayers.Add(new HumanPlayer(color, _control));
            return this;
        }
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log = false)
        {
            AddColor(color);

            if (log)
                _gamePlayers.Add(new Stephan(color, new StephanLog(color)));
            else
                _gamePlayers.Add(new Stephan(color));
            return this;
        }
        public IGameBuilderStartingColor SetUpPawns()
        {
            GameSetup.NewGame(Board.BoardSquares, _teamColors.ToArray());
            return this;
        }
        public IGameBuilderGamePlay DisableSaving() => this;
        public IGameBuilderGamePlay EnableSavingToDb()
        {
            _enableSaving = true;
            return this;
        }

        public GamePlay ToGamePlay()
        {
            var firstPlayer = _gamePlayers.Find(x => x.Color == _first);
            var gamePlay = new GamePlay(_gamePlayers, _dice, firstPlayer);
            if (_enableSaving == true) DatabaseManagement.SaveInit(gamePlay);
            return gamePlay;
        }
    }
}
