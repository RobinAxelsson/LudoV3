using LudoConsole.ServerMapping;
using LudoConsole.Ui;
using LudoEngine.ClientApi;
using LudoEngine.ClientApi.Dto;

namespace LudoConsole.Controller
{
    internal sealed class LudoConsoleClient : LudoClientBase
    {
        public override void OnNewGame(DtoLudoGame dtoLudoGame)
        {
            var consoleSquares = ConsoleDtoMapper.Map(dtoLudoGame);
            BoardRenderer.StartRender(consoleSquares);
        }

        public override void OnGetMove(DtoMove move)
        {
            throw new System.NotImplementedException();
        }
    }
}
