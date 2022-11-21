using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using LudoConsole.Main;
using LudoEngine.GameLogic.Dice;
using System;
using System.Collections.Generic;

namespace LudoTest.AI
{
    [Collection(nameof(StaticTestCollection))]
    public class AiTests
    {
        [Fact]
        public void Stephan_Choices_AssertErradicate()
        {
            Board.Init(@"AI/ai-test-map1.txt");

            var stephan = new Stephan(TeamColor.Blue, null);
            var dice = new RiggedDice(new[] { 2 });

            var pawn1 = new Pawn(TeamColor.Blue);
            var pawn2 = new Pawn(TeamColor.Blue);
            var enemyPawn = new Pawn(TeamColor.Green);
            var squarePawn1 = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            var squarePawn2 = Board.BoardSquares.Find(x => x.BoardX == 1 && x.BoardY == 1);
            var squareEnemy = Board.BoardSquares.Find(x => x.BoardX == 2 && x.BoardY == 1);
            var enemyBase = Board.BaseSquare(TeamColor.Green);

            squarePawn1.Pawns.Add(pawn1);
            squarePawn2.Pawns.Add(pawn2);
            squareEnemy.Pawns.Add(enemyPawn);

            stephan.Play(dice);

            Assert.True(squarePawn1.Pawns.Count == 0);

        }
        [Fact]
        public void StephanRollSix_AssertTakeOutTwo()
        {
            Board.Init(@"AI/ai-test-map1.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue });
            var dice = new RiggedDice(new[] { 6, 1});

            var stephan = new Stephan(TeamColor.Blue, null);
            stephan.Play(dice);
            var startSquare = Board.StartSquare(TeamColor.Blue);
            var pawns = startSquare.Pawns;
            Assert.True(pawns.Count == 2);
        }
    }
}
