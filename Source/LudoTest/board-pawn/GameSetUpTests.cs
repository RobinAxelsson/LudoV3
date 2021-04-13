
using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic.Dice;
using LudoEngine.Creation;
using System.Collections.Generic;
using LudoConsole.UI.Controls;
using LudoEngine.GameLogic.GamePlayers;

namespace LudoTest.board_pawn
{
    public class GameSetupTests
    {
        [Fact]
        public void LoadOnePawn_AssertTrue()
        {
            var savePoint = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue
            };
            GameBuilder.StartBuild()
                .MapBoard(@"board-pawn/test-copy_BoardMap.txt")
                .AddDice(new Dice(1, 6))
                .SetControl(null)
                .SetInfoDisplay(null)
                .LoadGame()
                .LoadPawns(new List<PawnSavePoint> { savePoint });

            var pawns = Board.GetTeamPawns(TeamColor.Blue);
            Assert.True(pawns.Count == 1);
        }
        [Fact]
        public void LoadTwoPawn_Stands_AssertTrue()
        {
            var savePoint = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue
            };
            var savePoint2 = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue
            };
            var loaded = GameBuilder.StartBuild()
                .MapBoard(@"board-pawn/test-copy_BoardMap.txt")
                .AddDice(new Dice(1, 6))
                .SetControl(null)
                .SetInfoDisplay(null)
                .LoadGame()
                .LoadPawns(new List<PawnSavePoint> { savePoint, savePoint2 });

            var square = Board.BoardSquares.Find(x => x.BoardX == savePoint.XPosition && x.BoardY == savePoint.YPosition);

            Assert.True(square.Pawns.Count == 2);
        }
        [Fact]
        public void LoadHumanPlayer_AssertTrue()
        {
            var savePoint = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue,
            };
            var game = GameBuilder.StartBuild()
            .MapBoard(@"board-pawn/test-copy_BoardMap.txt")
            .AddDice(new Dice(1, 6))
            .SetControl(ConsoleDefaults.KeyboardControl)
            .SetInfoDisplay(ConsoleDefaults.display)
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePoint })
            .LoadPlayers()
            .StartingColor(TeamColor.Blue)
            .DisableSaving()
            .ToGamePlay();

            var players = game.Players;

            Assert.True(players.Count == 1);
        }
        [Fact]
        public void Load3HumanPlayer_AssertTrue()
        {
            var savePoint = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue
            };
            var savePoint2 = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 3,
                Color = TeamColor.Red
            };
            var savePoint3 = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 2,
                Color = TeamColor.Yellow
            };
            var savePoint4 = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 1,
                Color = TeamColor.Yellow
            };
            var game = GameBuilder.StartBuild()
            .MapBoard(@"board-pawn/test-copy_BoardMap.txt")
            .AddDice(new Dice(1, 6))
            .SetControl(ConsoleDefaults.KeyboardControl)
            .SetInfoDisplay(ConsoleDefaults.display)
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePoint, savePoint2, savePoint3, savePoint4 })
            .LoadPlayers()
            .StartingColor(TeamColor.Blue)
            .DisableSaving()
            .ToGamePlay();

            var players = game.Players;

            Assert.True(players.Count == 3);
        }
        [Fact]
        public void Load1HumanPlayer1Ai_AssertTrue()
        {
            var savePointHuman = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 4,
                Color = TeamColor.Blue,
                PlayerType = 0
                
            };
            var savePointStephan = new PawnSavePoint()
            {
                XPosition = 4,
                YPosition = 3,
                Color = TeamColor.Red,
                PlayerType = 1

            };
            
            var game = GameBuilder.StartBuild()
            .MapBoard(@"board-pawn/test-copy_BoardMap.txt")
            .AddDice(new Dice(1, 6))
            .SetControl(ConsoleDefaults.KeyboardControl)
            .SetInfoDisplay(ConsoleDefaults.display)
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePointHuman, savePointStephan })
            .LoadPlayers()
            .StartingColor(TeamColor.Blue)
            .EnableSavingToDb()
            .ToGamePlay();

            var gamePlayers = game.Players;
            var aiExists = game.Players.Exists(x => x is Stephan);

            Assert.True(aiExists == true);
        }

    }
}
