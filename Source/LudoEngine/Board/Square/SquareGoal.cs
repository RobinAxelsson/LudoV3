using LudoEngine.Enum;
namespace LudoEngine.Board.Square
{
    internal sealed class SquareGoal : SquareBase
    {
        public SquareGoal(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public override BoardDirection DirectionNext(TeamColor color)
        {
                return
                   Color == TeamColor.Yellow ? BoardDirection.Up :
                   Color == TeamColor.Blue ? BoardDirection.Right :
                   Color == TeamColor.Red ? BoardDirection.Down : BoardDirection.Left;
        }
    }
}