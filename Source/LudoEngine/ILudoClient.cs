using System.Collections.Generic;
using LudoEngine.Enum;

namespace LudoConsole
{
    public interface ILudoClient
    {
        //gameSquares Dtos
        public void OnReceiveGameSquares()
        {

        }

        //pawnDtos
        public void OnReceiveUpdatedPawns(IEnumerable<PawnDto> pawnDtos);

        //pawnDto
        public void OnMakeMove(int diceRoll)
        {

        }


    }

    public record GameSquareDto(int BoardX, int BoardY, TeamColor Color);
    public record PawnDto(int Id, int X, int Y, TeamColor Color);
}

public enum SquareType
{

}