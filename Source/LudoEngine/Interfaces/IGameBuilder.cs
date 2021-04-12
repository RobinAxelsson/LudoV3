using LudoConsole.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;

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
        public IGameBuilderLoadPlayers LoadPawns(List<PawnSavePoint> savePoints);
        public IGameBuilderNewGame NewGame();
    }
    public interface IGameBuilderNewGame
    {
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color);
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log);
        public IGameBuilderStartingColor SetUpPawns();
    }
    public interface IGameBuilderLoadPlayers
    {
        public IGameBuilderStartingColor LoadPlayers();
    }
 
    public interface IGameBuilderNewGamePlay
    {
        public IGameBuilderNewGamePlay AddHumanPlayer(TeamColor color);
        public IGameBuilderNewGamePlay AddAIPlayer(TeamColor color, bool log);
        public IGameBuilderStartingColor SetUpPawns();
    }
    public interface IGameBuilderStartingColor
    {
        public IGameBuilderRunsWhile StartingColor(TeamColor? color);
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
