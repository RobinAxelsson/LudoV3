using LudoConsole.Main;
using LudoEngine.BoardUnits.Main;
using LudoEngine.DbModel;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoEngine.Creation
{
    public class GameBuilder
    {
        private IDice _dice { get; set; }
        private List<TeamColor> _teamColors { get; set; } = new();
        private List<PawnSavePoint> _pawnSavePoints { get; set; } = new();
        private TeamColor _first { get; set; }
        private List<IGamePlayer> _gamePlayers { get; set; } = new();
        private bool _enableSaving { get; set; }
        private void AddColor(TeamColor color)
        {
            if (_teamColors.Contains(color)) throw new Exception("There can only be one player per color");
            _teamColors.Add(color);
        }
        public static GameBuilder StartBuild() => new GameBuilder();
        public GameBuilder MapBoard(string filePath)
        {
            Board.BoardSquares = BoardOrm.Map(filePath);
            return this;
        }
        public GameBuilder AddDice(IDice dice)
        {
            _dice = dice;
            return this;
        }
        //public GameBuilder SetInfoDisplay(IInfoDisplay infoDisplay)
        //{
        //    return this;
        //}
        public GameBuilder LoadGame()
        {
            return this;
        }
        public GameBuilder LoadPawns(List<PawnSavePoint> savePoints)
        {
            GameSetup.LoadSavedPawns(savePoints);
            _pawnSavePoints = savePoints;
            
            return this;
        }
        public GameBuilder LoadPlayers(IController humanController)
        {
            var colorTypeList = _pawnSavePoints.Select(x => (x.Color, x.PlayerType)).Distinct().ToList();

            foreach (var colorType in colorTypeList)
            {
                if (colorType.PlayerType == 0)
                {
                    AddHumanPlayer(colorType.Color, humanController);
                }
                if (colorType.PlayerType == 1)
                {
                    AddAIPlayer(colorType.Color);
                }
            }
            return this;
        }

        private void AddHumanPlayer(TeamColor color, object consoleDefaults)
        {
            throw new NotImplementedException();
        }

        public GameBuilder NewGame()
        {
            return this;
        }
        public GameBuilder StartingColor(TeamColor? color)
        {
            if (color != null && _teamColors.Contains((TeamColor)color)) _first = (TeamColor)color;
            else _first = _teamColors[0];
            if (!_teamColors.Contains((TeamColor)color)) throw new Exception("Teamcolor for first player is not present in the game");

            return this;
        }
        public GameBuilder AddHumanPlayer(TeamColor color, IController control)
        {
            AddColor(color);
            _gamePlayers.Add(new HumanPlayer(color,  control));
            return this;
        }
        public GameBuilder AddAIPlayer(TeamColor color, bool log = false)
        {
            AddColor(color);

            if (log)
                _gamePlayers.Add(new Stephan(color, new StephanLog(color)));
            else
                _gamePlayers.Add(new Stephan(color));
            return this;
        }
        public GameBuilder SetUpPawns()
        {
            GameSetup.NewGame(Board.BoardSquares, _teamColors.ToArray());
            return this;
        }
        public GameBuilder DisableSaving() => this;
        public GameBuilder EnableSavingToDb()
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
