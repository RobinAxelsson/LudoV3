using LudoEngine.Board;
using LudoEngine.Enum;
using LudoEngine.Exceptions;
using LudoEngine.GameLogic;
using LudoEngine.GameLogic.GamePlayers;
using Xunit;

namespace LudoTest
{
    [Collection(nameof(LudoXUnitCollection))]
    public class LudoFullGameTests
    {
        [Fact]
        public void Run_full_game()
        {
            var aiPlayers = new[]
            {
                new Stephan(TeamColor.Red),
                new Stephan(TeamColor.Blue),
                new Stephan(TeamColor.Yellow),
                new Stephan(TeamColor.Green),
            };

            var gamePlay = new GamePlay(aiPlayers, new Dice(1,6));

            GameBoard.Init();
            GameSetup.SetUpPawnsNewGame(GameBoard.BoardSquares);
            Assert.Throws<NoPlayersException>(() => gamePlay.Start());
        }
    }
}