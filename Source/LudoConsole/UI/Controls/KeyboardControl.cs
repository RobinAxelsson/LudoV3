using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;
using System;
using System.Collections.Generic;

namespace LudoConsole.UI.Controls
{
    public class KeyboardControl : IController
    {
        public void Throw() => Console.ReadKey(true);
        private Action<string> DisplayMessage { get; set; }
        public KeyboardControl(Action<string> displayMessage)
        {
            DisplayMessage = displayMessage;
        }
        private void DeselectAll(List<Pawn> pawns) => pawns.ForEach(x => x.IsSelected = false);
        public List<Pawn> Select(List<Pawn> pawns, bool takeTwo)
        {
            var key = new ConsoleKeyInfo().Key;
            var outPawns = new List<Pawn>();
            int selection = 0;
            DeselectAll(pawns);
            pawns[selection].IsSelected = true;

            while (true)
            {
                if (takeTwo == true)
                {
                    DisplayMessage("'x' for two");
                    if (key == ConsoleKey.X)
                    {
                        var basePawns = pawns.FindAll(x => x.Based() == true);
                        outPawns.Add(basePawns[0]);
                        outPawns.Add(basePawns[1]);
                        DeselectAll(pawns);
                        return outPawns;
                    }
                }
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow)
                    selection++;

                if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow)
                    selection--;

                selection =
                    selection > pawns.Count - 1 ? 0 :
                    selection < 0 ? pawns.Count - 1 : selection;
                
                DeselectAll(pawns);
                pawns[selection].IsSelected = true;
                

                if (key == ConsoleKey.Enter)
                {
                    var output = pawns.FindAll(x => x.IsSelected == true);
                    DeselectAll(pawns);
                    return output;
                }
            }
        }
    }
}