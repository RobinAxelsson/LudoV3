using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using System.Collections.Generic;
using LudoEngine.Board;
using LudoEngine.Interfaces;

namespace LudoTest.AI
{
    [Collection(nameof(LudoXUnitCollection))]
    public class AiTests
    {
        private List<IGameSquare> BoardSquares => GameBoard.BoardSquares;

        [Fact]
        public void Stephan_Choices_AssertErradicate()
        {
            GameBoard.Init(@"AI/ai-test-map1.txt");

            var stephan = new Stephan(TeamColor.Blue, null);
            var dice = new DiceRigged(new[] { 2 });

            var pawn1 = new Pawn(TeamColor.Blue);
            var pawn2 = new Pawn(TeamColor.Blue);
            var enemyPawn = new Pawn(TeamColor.Green);
            var squarePawn1 = BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            var squarePawn2 = BoardSquares.Find(x => x.BoardX == 1 && x.BoardY == 1);
            var squareEnemy = BoardSquares.Find(x => x.BoardX == 2 && x.BoardY == 1);
            var enemyBase = GameBoard.BaseSquare(BoardSquares, TeamColor.Green);

            squarePawn1.Pawns.Add(pawn1);
            squarePawn2.Pawns.Add(pawn2);
            squareEnemy.Pawns.Add(enemyPawn);

            stephan.Play(dice);

            Assert.Empty(squarePawn1.Pawns);

        }
        [Fact]
        public void StephanRollSix_AssertTakeOutTwo()
        {
            GameBoard.Init(@"AI/ai-test-map1.txt");
            var squares = BoardSquares;
            GameSetup.SetUpPawnsNewGame(squares, new [] { TeamColor.Blue });
            var dice = new DiceRigged(new[] { 6, 1});

            var stephan = new Stephan(TeamColor.Blue, null);
            stephan.Play(dice);
            var startSquare = GameBoard.StartSquare(BoardSquares, TeamColor.Blue);
            var pawns = startSquare.Pawns;
            Assert.True(pawns.Count == 2);
        }
    }
}
