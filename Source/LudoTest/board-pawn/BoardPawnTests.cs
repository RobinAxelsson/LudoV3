
using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;

namespace LudoTest.board_pawn
{
    [Collection(nameof(StaticTestCollection))]
    public class BoardPawnTests
    {
        [Fact]
        public void MoveToExit_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = BoardNavigation.BaseSquare(Board.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(7);
            var current = bluePawn.CurrentSquare();

            Assert.IsType<ExitSquare>(current);
        }
        [Fact]
        public void MoveToFinish_AndRemoveFromBoard_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = BoardNavigation.BaseSquare(Board.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(8);
            var current = bluePawn.CurrentSquare();

            Assert.True(current == null);
        }
        [Fact]
        public void BlueBounceFromFinish_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map4.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = BoardNavigation.BaseSquare(Board.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);
            bluePawn.Move(7);
            var expectedSquare = Board.BoardSquares[1];
            var square = bluePawn.CurrentSquare();

            Assert.True(expectedSquare == square);
        }
        [Fact]
        public void RedExitSquare_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = BoardNavigation.StartSquare(Board.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(1);
            var square = BoardPawnFinder.FindPawnSquare(Board.BoardSquares, redPawn);
            Assert.IsType<ExitSquare>(square);
        }
        [Fact]
        public void RedSafeZoneSquare_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = BoardNavigation.StartSquare(Board.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(2);
            var square = BoardPawnFinder.FindPawnSquare(Board.BoardSquares, redPawn);
            Assert.IsType<SafezoneSquare>(square);
        }
        [Fact]
        public void RedGoal_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = BoardNavigation.StartSquare(Board.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            redPawn.Move(3);
            var pawns = BoardPawnFinder.AllBaseAndPlayingPawns(Board.BoardSquares);
            Assert.True(pawns.Count == 0);
        }
        [Fact]
        public void RedGoalBounce_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = BoardNavigation.StartSquare(Board.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            var squarse = Board.BoardSquares;
            redPawn.Move(4);
            var expectedSquare = Board.BoardSquares[2];

            Assert.True(expectedSquare.Pawns.Count == 1);
        }
        [Fact]
        public void RedGoalBounce2_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map3.txt");
            var redPawn = new Pawn(TeamColor.Red);
            var startSquare = BoardNavigation.StartSquare(Board.BoardSquares, TeamColor.Red);
            startSquare.Pawns.Add(redPawn);

            var squares = Board.BoardSquares;
            redPawn.Move(5);
            var expectedSquare = Board.BoardSquares[1];

            Assert.True(expectedSquare.Pawns.Count == 1);
        }
        [Fact]
        public void MoveUpNotFinish_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            var redPawn = new Pawn(TeamColor.Red);
            var start = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);

            redPawn.Move(7);
            var current = redPawn.CurrentSquare();

            Assert.IsType<StandardSquare>(current);
        }
        [Fact]
        public void ErradicateOne_AssertBaseSquare()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = BoardNavigation.BaseSquare(Board.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            var redPawn = new Pawn(TeamColor.Red);
            var start = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);

            bluePawn.Move(1);
            var current = redPawn.CurrentSquare();

            Assert.IsType<BaseSquare>(current);
        }
        [Fact]
        public void ErradicateTwo_AssertTwoInBase()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = BoardNavigation.BaseSquare(Board.BoardSquares, TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            var redPawn = new Pawn(TeamColor.Red);
            var redPawn2 = new Pawn(TeamColor.Red);
            var start = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);
            start.Pawns.Add(redPawn2);

            bluePawn.Move(1);
            var redsBased = BoardPawnFinder.PawnsInBase(Board.BoardSquares, TeamColor.Red);

            Assert.True(redsBased.Count == 2);
        }
        [Fact]
        public void GameSetup_fourRed_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Red });
            var redsBased = BoardPawnFinder.PawnsInBase(Board.BoardSquares, TeamColor.Red);

            Assert.True(redsBased.Count == 4);
        }
        [Fact]
        public void GameSetup_fourBlue_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var bluesBased = BoardPawnFinder.PawnsInBase(Board.BoardSquares, TeamColor.Blue);

            Assert.True(bluesBased.Count == 4);
        }
        [Fact]
        public void GameSetUp_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var greensBased = BoardPawnFinder.PawnsInBase(Board.BoardSquares, TeamColor.Green);

            Assert.True(greensBased.Count == 4);
        }
        [Fact]
        public void GetTeamPawns_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });

            var bluePawns = BoardPawnFinder.GetTeamPawns(Board.BoardSquares, TeamColor.Blue);

            Assert.True(bluePawns.Count == 4);
        }
    }
}
