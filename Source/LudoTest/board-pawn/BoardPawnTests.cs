using LudoEngine.Board;
using LudoEngine.Board.Square;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;

namespace LudoTest.board_pawn
{
    [Collection(nameof(LudoXUnitCollection))]
    public class BoardPawnTests
    {
        [Fact]
        public void MoveToExit_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = GameBoard.BaseSquare(GameBoard.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(7);
            var current = bluePawn.CurrentSquare();

            Assert.IsType<SquareExit>(current);
        }
        [Fact]
        public void MoveToFinish_AndRemoveFromBoard_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = GameBoard.BaseSquare(GameBoard.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(8);
            var current = bluePawn.CurrentSquare();

            Assert.True(current == null);
        }
        [Fact]
        public void BlueBounceFromFinish_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map4.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = GameBoard.BaseSquare(GameBoard.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);
            bluePawn.Move(7);
            var expectedSquare = GameBoard.BoardSquares[1];
            var square = bluePawn.CurrentSquare();

            Assert.True(expectedSquare == square);
        }
        [Fact]
        public void RedExitSquare_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = GameBoard.StartSquare(GameBoard.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(1);
            var square = GameBoard.FindPawnSquare(GameBoard.BoardSquares, redPawn);
            Assert.IsType<SquareExit>(square);
        }
        [Fact]
        public void RedSafeZoneSquare_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = GameBoard.StartSquare(GameBoard.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(2);
            var square = GameBoard.FindPawnSquare(GameBoard.BoardSquares, redPawn);
            Assert.IsType<SquareSafeZone>(square);
        }
        [Fact]
        public void RedGoal_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = GameBoard.StartSquare(GameBoard.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(3);
            var pawns = GameBoard.AllBaseAndPlayingPawns(GameBoard.BoardSquares);
            Assert.True(pawns.Count == 0);
        }
        [Fact]
        public void RedGoalBounce_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = GameBoard.StartSquare(GameBoard.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            var squares = GameBoard.BoardSquares;
            redPawn.Move(4);
            var expectedSquare = GameBoard.BoardSquares[2];

            Assert.True(expectedSquare.Pawns.Count == 1);
        }
        [Fact]
        public void RedGoalBounce2_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = GameBoard.StartSquare(GameBoard.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            var squares = GameBoard.BoardSquares;
            redPawn.Move(5);
            var expectedSquare = GameBoard.BoardSquares[1];

            Assert.True(expectedSquare.Pawns.Count == 1);
        }
        [Fact]
        public void MoveUpNotFinish_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var squares = GameBoard.BoardSquares;
            var redPawn = new Pawn(TeamColor.Red);
            var start = GameBoard.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);

            redPawn.Move(7);
            var current = redPawn.CurrentSquare();

            Assert.IsType<SquareStandard>(current);
        }
        [Fact]
        public void ErradicateOne_AssertBaseSquare()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var squares = GameBoard.BoardSquares;
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = GameBoard.BaseSquare(GameBoard.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            var redPawn = new Pawn(TeamColor.Red);
            var start = GameBoard.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);

            bluePawn.Move(1);
            var current = redPawn.CurrentSquare();

            Assert.IsType<SquareTeamBase>(current);
        }
        [Fact]
        public void ErradicateTwo_AssertTwoInBase()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var squares = GameBoard.BoardSquares;
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = GameBoard.BaseSquare(GameBoard.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            var redPawn = new Pawn(TeamColor.Red);
            var redPawn2 = new Pawn(TeamColor.Red);
            var start = GameBoard.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);
            start.Pawns.Add(redPawn2);

            bluePawn.Move(1);
            var redsBased = GameBoard.PawnsInBase(GameBoard.BoardSquares, TeamColor.Red);

            Assert.True(redsBased.Count == 2);
        }
        [Fact]
        public void GameSetup_fourRed_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map1.txt");
            var squares = GameBoard.BoardSquares;
            GameSetup.SetUpPawnsNewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Red });
            var redsBased = GameBoard.PawnsInBase(GameBoard.BoardSquares, TeamColor.Red);

            Assert.True(redsBased.Count == 4);
        }
        [Fact]
        public void GameSetup_fourBlue_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map-2p.txt");
            var squares = GameBoard.BoardSquares;
            GameSetup.SetUpPawnsNewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var bluesBased = GameBoard.PawnsInBase(GameBoard.BoardSquares, TeamColor.Blue);

            Assert.True(bluesBased.Count == 4);
        }
        [Fact]
        public void GameSetUp_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map-2p.txt");
            var squares = GameBoard.BoardSquares;
            GameSetup.SetUpPawnsNewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var greensBased = GameBoard.PawnsInBase(GameBoard.BoardSquares, TeamColor.Green);

            Assert.True(greensBased.Count == 4);
        }
        [Fact]
        public void GetTeamPawns_AssertTrue()
        {
            GameBoard.Init(@"board-pawn/test-map-2p.txt");
            var squares = GameBoard.BoardSquares;
            GameSetup.SetUpPawnsNewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });

            var bluePawns = GameBoard.GetTeamPawns(GameBoard.BoardSquares, TeamColor.Blue);

            Assert.True(bluePawns.Count == 4);
        }
    }
}
