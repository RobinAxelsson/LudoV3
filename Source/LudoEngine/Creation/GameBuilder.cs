using LudoConsole.Main;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.Creation
{
    public class GameBuilder :
        IGameBuilderMapBoard,
        IGameBuilderAddDice,
        IGameBuilderSetControl,
        IGameBuilderSetInfoDisplay,
        IGameBuilderLoadOrNew,
        IGameBuilderGamePlay,
        IGameBuilderAddPlayer,
        IGameBuilderNewGamePlay,
        IGameBuilderStartingColor,
        IGameBuilderLoadPlayers,
        IGameBuilderRunsWhile
    {
        private IController _control { get; set; }
        private IDice _dice { get; set; }
        private IInfoDisplay _display { get; set; }
        private List<TeamColor> _teamColors { get; set; } = new();
        private TeamColor _first { get; set; }
        private List<IGamePlayer> _gamePlayers { get; set; } = new();
        private Func<bool> _runsWhileCondtition { get; set; }
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
        public IGameBuilderLoadPlayers LoadPawns(List<PawnSavePoint> savePoints) //TODO needs setup logic
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
                 _gamePlayers.Add(new HumanPlayer(color, _display.UpdateDiceRoll, _control));
            }
            //HumanColors = savePoints.Select(x => x.Color && x.PlayerType == 0).Distinct().ToList();
            //AiColors = savePoints.Select(x => x.Color && x.PlayerType == 1).Distinct().ToList();
            return this;
        }
        public IGameBuilderAddPlayer NewGame()
        {
            return this;
        }
        public IGameBuilderRunsWhile StartingColor(TeamColor? color)
        {
            if (color != null && _teamColors.Contains((TeamColor)color)) _first = (TeamColor)color;
            else _first = _teamColors[0];
            if (!_teamColors.Contains((TeamColor)color)) throw new Exception("Teamcolor for first player is not present in the game");

            return this;
        }
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color)
        {
            AddColor(color);
            _gamePlayers.Add(new HumanPlayer(color, _display.UpdateDiceRoll, _control));
            return this;
        }
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log = false)
        {
            AddColor(color);

            if (log)
                _gamePlayers.Add(new Stephan(color, _display.UpdateDiceRoll, new StefanLog(color)));
            else
                _gamePlayers.Add(new Stephan(color, _display.UpdateDiceRoll));
            return this;
        }
        public IGameBuilderStartingColor SetUpPawns()
        {
            GameSetup.NewGame(Board.BoardSquares, _teamColors.ToArray());
            return this;
        }
        public IGameBuilderGamePlay GameRunsWhile(Func<bool> whileCondition)
        {
            _runsWhileCondtition = whileCondition;
            return this;
        }
        public GamePlay ToGamePlay()
        {
            var firstPlayer = _gamePlayers.Find(x => x.Color == _first);
            return new GamePlay(_gamePlayers, _dice, _runsWhileCondtition, firstPlayer);
        }
    }
}
