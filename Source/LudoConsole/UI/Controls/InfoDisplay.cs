using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LudoConsole.UI.Controls
{
    public class InfoDisplay
    {
        public InfoDisplay(int x, int y)
        {
            X = x;
            Y = y;
        }
        private List<IDrawable> drawables { get; set; } = new List<IDrawable>();
        private int X { get; set; }
        private int Y { get; set; }
        public void UpdateDiceRoll(TeamColor color, int diceRoll)
        {
            Update($"{color}, throw dice");
            Console.ReadKey(true);
            Update($"{color} throws...");
            Thread.Sleep(1000);
            Update($"{color} got a {diceRoll}");
            Thread.Sleep(500);
        }
        public void UpdateAIDiceRoll(TeamColor color, int diceRoll)
        {
            Thread.Sleep(500);
            Update($"{color} throws...");
            Thread.Sleep(1000);
            Update($"{color} got a {diceRoll}");
            Thread.Sleep(1000);
        }

        public void Update(string newString)
        {
            if (drawables.Count > newString.Length)
            {
                var iStart = newString.Length - 1;
                var end = drawables.Count;
                for (int i = iStart; i < end; i++)
                {
                    drawables[i].Erase = true;
                }
            }
            drawables.Clear();
            int x = 0;
            foreach (char chr in newString)
            {
                drawables.Add(new TextDrawable(this.X + x, this.Y, chr));
                x++;
            }
            ConsoleWriter.TryAppend(drawables);
        }
    }
}