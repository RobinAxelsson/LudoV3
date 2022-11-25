using LudoEngine.Enum;

namespace LudoEngine.ClientApi
{
    public record DtoGameSquare(int BoardX, int BoardY, LudoColor Color, SquareType SquareType);
}