using LudoEngine.Enums;

namespace LudoEngine.Board.Square
{
    internal sealed class GameSquareSafeZone : GameSquareBase
    {
        public GameSquareSafeZone(int boardX, int boardY, TeamColor color, BoardDirection direction)
        {
            Color = color;
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }
        public override BoardDirection DirectionNext(TeamColor color) => DefaultDirection;
    }
}