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
        public IGameBuilderLoadPawns LoadGame();
        public IGameBuilderNewGame NewGame();
    }
    public interface IGameBuilderLoadPawns
    {
        public IGameBuilderLoadPlayers LoadPawns(List<PawnSavePoint> savePoints);
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
        public IGameBuilderSaveConfig StartingColor(TeamColor? color);
    }
    public interface IGameBuilderSaveConfig
    {
        public IGameBuilderGamePlay DisableSaving();
        public IGameBuilderGamePlay EnableSavingToDb();
    }
    public interface IGameBuilderGamePlay
    {
        public GamePlay ToGamePlay();
    }
}
