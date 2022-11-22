using LudoConsole.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsole.UI.Controls
{
    public class ConsoleWriter
    {
        private static List<IDrawable> ScreenMemory = new List<IDrawable>();
        public static void TryAppend(List<SquareDrawableBase> squares)
        {
            var drawables = (squares.Select(x => x.Refresh()).SelectMany(x => x));
            TryAppend(drawables.ToList());
        }
        public static void TryAppend(IDrawable tryUnit)
        {
            if (IsInScreenMemory(tryUnit)) return;

            var oldUnit = ScreenMemory.Find(x =>
                x.CoordinateY == tryUnit.CoordinateY && x.CoordinateX == tryUnit.CoordinateX);

            if (oldUnit != null)
                ScreenMemory.Remove(oldUnit);

            ScreenMemory.Add(tryUnit);
        }
        public static void TryAppend(List<IDrawable> drawables)
        {
            drawables.ForEach(x => TryAppend(x));
        }
        public static void UpdateBoard(List<SquareDrawableBase> squareDrawables)
        {
            TryAppend(squareDrawables);
            Update();
        }
        public static void Update()
        {
            var toRemove = new List<IDrawable>();
            int countedMemory = ScreenMemory.Count;

            for (int i = 0; i < countedMemory; i++)
            {
                var drawable = ScreenMemory[i];

                if (drawable.Erase == false && drawable.IsDrawn == false)
                {
                    Write(drawable);
                }
                else if (drawable.Erase == true)
                {
                    Erase(drawable);
                    toRemove.Add(drawable);
                }
            }
            toRemove.ForEach(x => ScreenMemory.Remove(x));
        }
        public static void ClearScreen()
        {
            ScreenMemory.Clear();
            Console.Clear();
        }
        public static void EraseRows(int first, int last) => ScreenMemory.FindAll(x => x.CoordinateY >= first && x.CoordinateY <= last).ForEach(x => x.Erase = true);
        private static bool IsInScreenMemory(IDrawable drawable)
        {
            int countedMemory = ScreenMemory.Count;

            for (int i = 0; i < countedMemory; i++)
            {
                if (countedMemory < ScreenMemory.Count) countedMemory = ScreenMemory.Count;
                var drawableCompare = ScreenMemory[i];
                if (drawable.IsSame(drawableCompare))
                    return true;
            }
            return false;
        }
        private static void Write(IDrawable drawable)
        {
            Console.ForegroundColor = drawable.ForegroundColor;
            Console.BackgroundColor = drawable.BackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(drawable.Chars);
            drawable.IsDrawn = true;
            Console.ForegroundColor = UiColor.DefaultForegroundColor;
            Console.BackgroundColor = UiColor.DefaultBackgroundColor;
        }
        private static void Erase(IDrawable drawable)
        {
            Console.BackgroundColor = UiColor.DefaultBackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(" ");
            Console.ForegroundColor = UiColor.DefaultForegroundColor;
            drawable.IsDrawn = false;
            drawable.Erase = false;
        }
    }
}