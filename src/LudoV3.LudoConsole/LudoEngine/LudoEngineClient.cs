using System.Collections.Generic;
using System.Linq;
using LudoConsole.Model;
using LudoConsole.View;
using LudoConsole.View.Components;
using LudoEngine.ClientApi;
using LudoEngine.ClientApi.Dto;

namespace LudoConsole.LudoEngine
{
    internal sealed class LudoEngineClient : LudoClientBase
    {
        private List<ConsolePawn> _pawns;
        private IReadOnlyList<ViewGameSquareBase> _gameSquares;

        public override void OnNewGame(DtoLudoGame dtoLudoGame)
        {
            var consoleSquares = LudoEngineMapper.Map(dtoLudoGame).ToList();
            _pawns = consoleSquares.SelectMany(x => x.Pawns).ToList();
            var uiGameSquares = ViewGameSquareFactory.CreateUiGameSquares(consoleSquares).ToArray();
            _gameSquares = uiGameSquares;
            BoardRenderer.StartRender(uiGameSquares);
        }

        public override void OnGetMove(DtoMove move)
        {
            //var first = _pawns[0];
            //var square = _gameSquares.Single()
        }
    }
}
