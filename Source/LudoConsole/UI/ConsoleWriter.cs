using LudoConsole.UI.Components;
using LudoConsole.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsole.UI
{
    internal static class ConsoleWriter
    {
        private static readonly List<DrawableCharPoint> ScreenMemory = new();
        public static void TryAppend(List<BoardSquareBase> squares)
        {
            var drawables = squares.Select(x => x.Refresh()).SelectMany(x => x);
            TryAppend(drawables.ToList());
        }
        public static void TryAppend(DrawableCharPoint tryUnit)
        {
            if (IsInScreenMemory(tryUnit)) return;

            var oldUnit = ScreenMemory.Find(x =>
                x.CoordinateY == tryUnit.CoordinateY && x.CoordinateX == tryUnit.CoordinateX);

            if (oldUnit != null)
                ScreenMemory.Remove(oldUnit);

            ScreenMemory.Add(tryUnit);
        }
        public static void TryAppend(List<DrawableCharPoint> drawables)
        {
            drawables.ForEach(x => TryAppend(x));
        }
        public static void UpdateBoard(List<BoardSquareBase> squareDrawables)
        {
            TryAppend(squareDrawables);
            Update();
        }
        public static void Update()
        {
            var toRemove = new List<DrawableCharPoint>();
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

        public static void EraseRows(int first, int last) => ScreenMemory.FindAll(x => x.CoordinateY >= first && x.CoordinateY <= last).ForEach(x => x.DoErase = true);
        private static bool IsInScreenMemory(DrawableCharPoint drawableCharPoint)
        {
            int countedMemory = ScreenMemory.Count;

            for (int i = 0; i < countedMemory; i++)
            {
                if (countedMemory < ScreenMemory.Count) countedMemory = ScreenMemory.Count;
                var drawableCompare = ScreenMemory[i];
                if (drawableCharPoint.Equals(drawableCompare))
                    return true;
            }
            return false;
        }

        private static void Write(DrawableCharPoint drawableCharPoint)
        {
            Console.ForegroundColor = drawableCharPoint.ForegroundColor;
            Console.BackgroundColor = drawableCharPoint.BackgroundColor;
            Console.SetCursorPosition(drawableCharPoint.CoordinateX, drawableCharPoint.CoordinateY);
            Console.Write(drawableCharPoint.Chars);
            drawableCharPoint.IsDrawn = true;
            Console.ForegroundColor = UiColor.DefaultForegroundColor;
            Console.BackgroundColor = UiColor.DefaultBackgroundColor;
        }

        private static void Erase(DrawableCharPoint drawableCharPoint)
        {
            Console.BackgroundColor = UiColor.DefaultBackgroundColor;
            Console.SetCursorPosition(drawableCharPoint.CoordinateX, drawableCharPoint.CoordinateY);
            Console.Write(" ");
            Console.ForegroundColor = UiColor.DefaultForegroundColor;
            drawableCharPoint.IsDrawn = false;
            drawableCharPoint.DoErase = false;
        }
    }
}