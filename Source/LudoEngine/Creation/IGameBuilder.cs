using LudoConsole.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using System;

namespace LudoEngine.Creation
{
    public interface IGameBuilderMapBoard
    {
        public IGameBuilderAddDice MapBoard(string filePath);
    }
    public interface IGameBuilderAddDice
    {
        public IGameBuilderSetControl AddDice(IDice dice);
    }
    public interface IGameBuilderSetControl
    {
        public IGameBuilderSetInfoDisplay SetControl(IController control);
    }
    public interface IGameBuilderSetInfoDisplay
    {
        public IGameBuilderLoadOrNew SetInfoDisplay(IInfoDisplay infoDisplay);
    }
    public interface IGameBuilderLoadOrNew
    {
        public IGameBuilderGamePlay LoadGame();
        public IGameBuilderAddPlayer NewGame();
    }
    public interface IGameBuilderAddPlayer
    {
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color);
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log);
    }
    public interface IGameBuilderNewGamePlay
    {
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color);
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log);
        public IGameBuilderSetupPawns StartingColor(TeamColor color);
    }
    public interface IGameBuilderSetupPawns
    {
        public IGameBuilderRunsWhile SetUpPawns();
    }
    public interface IGameBuilderRunsWhile
    {
        public IGameBuilderGamePlay GameRunsWhile(Func<bool> whileCondition);
    }
    public interface IGameBuilderGamePlay
    {
        public GamePlay ToGamePlay();
    }
}
