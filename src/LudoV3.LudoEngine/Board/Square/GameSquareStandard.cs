using LudoEngine.Enums;

namespace LudoEngine.Board.Square
{
    internal sealed class GameSquareStandard : GameSquareBase
    {
        public GameSquareStandard(int boardX, int boardY, BoardDirection direction)
        {
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }

        public override BoardDirection DirectionNext(TeamColor color) => DefaultDirection;
    }
}