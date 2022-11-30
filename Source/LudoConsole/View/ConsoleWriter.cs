using System;
using System.Collections.Generic;
using System.Linq;
using LudoConsole.View.Components;
using LudoConsole.View.Components.Models;

namespace LudoConsole.View
{
    internal static class ConsoleWriter
    {
        private static readonly List<ConsolePixel> ScreenMemory = new();

        public static void TryAppend(List<ViewGameSquareBase> squares)
        {
            var drawables = squares.Select(x => x.Refresh()).SelectMany(x => x);
            TryAppend(drawables.ToList());
        }

        public static void TryAppend(ConsolePixel tryUnit)
        {
            if (IsInScreenMemory(tryUnit)) return;

            var oldUnit = ScreenMemory.Find(x =>
                x.CoordinateY == tryUnit.CoordinateY && x.CoordinateX == tryUnit.CoordinateX);

            if (oldUnit != null)
                ScreenMemory.Remove(oldUnit);

            ScreenMemory.Add(tryUnit);
        }

        public static void TryAppend(List<ConsolePixel> drawables)
        {
            drawables.ForEach(x => TryAppend(x));
        }

        public static void UpdateBoard(List<ViewGameSquareBase> squareDrawables)
        {
            TryAppend(squareDrawables);
            Update();
        }

        public static void Update()
        {
            var toRemove = new List<ConsolePixel>();
            var countedMemory = ScreenMemory.Count;

            for (var i = 0; i < countedMemory; i++)
            {
                var drawable = ScreenMemory[i];

                if (drawable.DoErase == false && drawable.IsDrawn == false)
                {
                    Write(drawable);
                }
                else if (drawable.DoErase)
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

        public static void EraseRows(int first, int last)
        {
            ScreenMemory.FindAll(x => x.CoordinateY >= first && x.CoordinateY <= last).ForEach(x => x.DoErase = true);
        }

        private static bool IsInScreenMemory(ConsolePixel consolePixel)
        {
            var countedMemory = ScreenMemory.Count;

            for (var i = 0; i < countedMemory; i++)
            {
                if (countedMemory < ScreenMemory.Count) countedMemory = ScreenMemory.Count;
                var drawableCompare = ScreenMemory[i];
                if (consolePixel.Equals(drawableCompare))
                    return true;
            }

            return false;
        }

        private static void Write(ConsolePixel consolePixel)
        {
            Console.ForegroundColor = consolePixel.ForegroundColor;
            Console.BackgroundColor = consolePixel.BackgroundColor;
            Console.SetCursorPosition(consolePixel.CoordinateX, consolePixel.CoordinateY);
            Console.Write(consolePixel.Chars);
            consolePixel.IsDrawn = true;
            Console.ForegroundColor = ColorManager.DefaultForegroundColor;
            Console.BackgroundColor = ColorManager.DefaultBackgroundColor;
        }

        private static void Erase(ConsolePixel consolePixel)
        {
            Console.BackgroundColor = ColorManager.DefaultBackgroundColor;
            Console.SetCursorPosition(consolePixel.CoordinateX, consolePixel.CoordinateY);
            Console.Write(" ");
            Console.ForegroundColor = ColorManager.DefaultForegroundColor;
            consolePixel.IsDrawn = false;
            consolePixel.DoErase = false;
        }
    }
}