
using LudoEngine.BoardUnits.Main;
using LudoEngine.Models;
using LudoEngine.Enum;
using Xunit;
using LudoEngine.GameLogic;

namespace LudoTest.board_pawn
{
    public class BoardPawnTests
    {
        [Fact]
        public void MoveToExit_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = Board.BaseSquare(TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(7);
            var current = bluePawn.CurrentSquare();

            Assert.IsType<ExitSquare>(current);
        }
        [Fact]
        public void MoveToFinish_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            var bluePawn = new Pawn(TeamColor.Blue);
            var baseSquare = Board.BaseSquare(TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            bluePawn.Move(8);
            var current = bluePawn.CurrentSquare();

            Assert.IsType<GoalSquare>(current);
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
            var baseSquare = Board.BaseSquare(TeamColor.Blue);
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
            var baseSquare = Board.BaseSquare(TeamColor.Blue);
            baseSquare.Pawns.Add(bluePawn);

            var redPawn = new Pawn(TeamColor.Red);
            var redPawn2 = new Pawn(TeamColor.Red);
            var start = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);
            start.Pawns.Add(redPawn);
            start.Pawns.Add(redPawn2);

            bluePawn.Move(1);
            var redsBased = Board.PawnsInBase(TeamColor.Red);

            Assert.True(redsBased.Count == 2);
        }
        [Fact]
        public void GameSetup_fourRed_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map1.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Red });
            var redsBased = Board.PawnsInBase(TeamColor.Red);

            Assert.True(redsBased.Count == 4);
        }
        [Fact]
        public void GameSetup_fourBlue_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var bluesBased = Board.PawnsInBase(TeamColor.Blue);

            Assert.True(bluesBased.Count == 4);
        }
        [Fact]
        public void GameSetUp_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            var greensBased = Board.PawnsInBase(TeamColor.Green);

            Assert.True(greensBased.Count == 4);
        }
        [Fact]
        public void GetTeamPawns_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });

            var bluePawns = Board.GetTeamPawns(TeamColor.Blue);

            Assert.True(bluePawns.Count == 4);
        }
        [Fact]
        public void ActivePlayer_AssertTrue()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });

            var bluePawns = Board.GetTeamPawns(TeamColor.Blue);
            ActivePlayer.SelectPawn(bluePawns[0]);
            ActivePlayer.MoveSelectedPawn(12);

            var assertedSquare = Board.BoardSquares.Find(x => x.BoardX == 0 && x.BoardY == 1);

            Assert.True(assertedSquare == bluePawns[0].CurrentSquare());
        }
        [Fact]
        public void RollSix_AssertTakeOutTwo()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            ActivePlayer.SetFirstTeam(TeamColor.Blue);
            ActivePlayer.TakeOutTwo();
            int pawnsInBase = Board.PawnsInBase(TeamColor.Blue).Count;

            Assert.True(pawnsInBase == 2);
        }
        [Fact]
        public void RollSix_AssertTakeOutFour()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            ActivePlayer.SetFirstTeam(TeamColor.Blue);
            ActivePlayer.TakeOutTwo();
            ActivePlayer.TakeOutTwo();
            var pawnsInBase = Board.PawnsInBase(TeamColor.Blue);

            Assert.True(pawnsInBase.Count == 0);
        }
        [Fact]
        public void Erradicate_NewTurn_ExpectNoException()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            ActivePlayer.SetFirstTeam(TeamColor.Blue);
            var bluePawns = Board.GetTeamPawns(TeamColor.Blue);
            var greenPawns = Board.GetTeamPawns(TeamColor.Green);

            ActivePlayer.SelectPawn(bluePawns[0]);
            ActivePlayer.MoveSelectedPawn(7);
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(greenPawns[0]);
            ActivePlayer.MoveSelectedPawn(1);
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(bluePawns[0]);
            ActivePlayer.MoveSelectedPawn(7);
            //Assert Does Not Throw Exception
        }
        [Fact]
        public void ErradicateAll_NewTurn_ExpectNoException()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            ActivePlayer.SetFirstTeam(TeamColor.Blue);
            var bluePawns = Board.GetTeamPawns(TeamColor.Blue);
            var greenPawns = Board.GetTeamPawns(TeamColor.Green);

            foreach (var bp in bluePawns)
            {
                ActivePlayer.SelectPawn(bp);
                ActivePlayer.MoveSelectedPawn(7);
            }
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(greenPawns[0]);
            ActivePlayer.MoveSelectedPawn(1);
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(bluePawns[0]);
            ActivePlayer.MoveSelectedPawn(7);
            //Assert Does Not Throw Exception
        }
        [Fact]
        public void ErradicateAll__ExpectNoException()
        {
            Board.Init(@"board-pawn/test-map-2p.txt");
            var squares = Board.BoardSquares;
            GameSetup.NewGame(squares, new TeamColor[] { TeamColor.Blue, TeamColor.Green });
            ActivePlayer.SetFirstTeam(TeamColor.Blue);
            var bluePawns = Board.GetTeamPawns(TeamColor.Blue);
            var greenPawns = Board.GetTeamPawns(TeamColor.Green);

            foreach (var bp in bluePawns)
            {
                ActivePlayer.SelectPawn(bp);
                ActivePlayer.MoveSelectedPawn(7);
            }
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(greenPawns[0]);
            ActivePlayer.MoveSelectedPawn(1);
            ActivePlayer.NextTeam();

            ActivePlayer.SelectPawn(bluePawns[0]);
            ActivePlayer.MoveSelectedPawn(7);
            //Assert Does Not Throw Exception
        }
    }
}
