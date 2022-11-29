using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.Creation;
using System.Collections.Generic;
using LudoEngine.GameLogic.GamePlayers;
using System.Linq;
using LudoConsole.Controller;
using LudoEngine.Board;
using LudoEngine.GameLogic;

namespace LudoTest.board_pawn
{
    [Collection(nameof(LudoXUnitCollection))]
    public class GameSetupTests
    {
        [Fact]
        public void Dice_throws_even()
        {
            var dice = new Dice(1, 6);
            var diceRolls = Enumerable.Range(1, 6000).Aggregate(new List<int>(), (list, next) =>
             {
                 list.Add(dice.Roll());
                 return list;
             });
            var ones = diceRolls.Where(x => x == 1).ToList();
            var other = diceRolls.Where(x => x > 6 || x < 1).ToList();

            Assert.Empty(other);
            Assert.True(ones.Count() > 800);

        }
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
                
                .LoadGame()
                .LoadPawns(new List<PawnSavePoint> { savePoint });

            var pawns = GameBoard.GetTeamPawns(GameBoard.BoardSquares, TeamColor.Blue);
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
                
                .LoadGame()
                .LoadPawns(new List<PawnSavePoint> { savePoint, savePoint2 });

            var square = GameBoard.BoardSquares.Find(x => x.BoardX == savePoint.XPosition && x.BoardY == savePoint.YPosition);

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
            
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePoint })
            .LoadPlayers(new KeyboardController())
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
            
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePoint, savePoint2, savePoint3, savePoint4 })
            .LoadPlayers(new KeyboardController())
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
            
            .LoadGame()
            .LoadPawns(new List<PawnSavePoint> { savePointHuman, savePointStephan })
            .LoadPlayers(new KeyboardController())
            .StartingColor(TeamColor.Blue)
            .EnableSavingToDb()
            .ToGamePlay();

            var aiExists = game.Players.Exists(x => x is Stephan);

            Assert.True(aiExists == true);
        }

    }
}
