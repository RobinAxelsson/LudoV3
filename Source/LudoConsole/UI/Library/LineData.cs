using System;
using System.Collections.Generic;

namespace LudoConsole.UI
{
    public class LineData
    {
        public LineData(int x, int y)
        {
            X = x;
            Y = y;
        }
        private List<IDrawable> drawables { get; set; } = new List<IDrawable>();
        private int X { get; set; }
        private int Y { get; set; }

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