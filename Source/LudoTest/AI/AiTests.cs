using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;

namespace LudoTest.AI
{
    public class AiTests
    {
        [Fact]
        public void Stephan_Choices_AssertErradicate()
        {
            Board.Init(@"AI/ai-test-map1.txt");

            var aiPawn1 = new Pawn(TeamColor.Red);
            var aiPawn2 = new Pawn(TeamColor.Red);
            var enemyPawn = new Pawn(TeamColor.Blue);
            var squareAi1 = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 0);
            var squareAi2 = Board.BoardSquares.Find(x => x.BoardX == 3 && x.BoardY == 0);
            var squareEnemy = Board.BoardSquares.Find(x => x.BoardX == 2 && x.BoardY == 0);
            squareAi1.Pawns.Add(aiPawn1);
            squareAi2.Pawns.Add(aiPawn2);
            squareEnemy.Pawns.Add(enemyPawn);

        }
        [Fact]
        public void StephanRollSix_AssertTakeOutTwo()
        {
            Board.Init(@"AI/ai-test-map2.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue });
            var stephan = new Stephan(TeamColor.Blue);
            stephan.TakeOutSquare = Board.StartSquare(TeamColor.Blue);
            stephan.Play(); //input dice
        }
    }
}
