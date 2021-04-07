using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsole.UI
{
    public static class ConsoleWriter
    {
        private static List<IDrawable> ScreenMemory = new List<IDrawable>();
        public static void TryAppend(List<ISquareDrawable> squares)
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
        public static void UpdateBoard(List<ISquareDrawable> squareDrawables)
        {
            //squareDrawables.ForEach(x => x.Refresh());
            TryAppend(squareDrawables);
            Update();
        }
        public static void Update()
        {
            var toRemove = new List<IDrawable>();
            foreach (var drawable in ScreenMemory)
                if (drawable.Erase == false && drawable.IsDrawn == false)
                {
                    Write(drawable);
                }
                else if (drawable.Erase)
                {
                    Erase(drawable);
                    toRemove.Add(drawable);
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
            foreach (var item in ScreenMemory)
                if (drawable.IsSame(item))
                    return true;
            return false;
        }
        private static void Write(IDrawable drawable)
        {
            Console.ForegroundColor = drawable.ForegroundColor;
            Console.BackgroundColor = drawable.BackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(drawable.Chars);
            drawable.IsDrawn = true;
            Console.ForegroundColor = UiControl.DefaultForegroundColor;
            Console.BackgroundColor = UiControl.DefaultBackgroundColor;
        }
        private static void Erase(IDrawable drawable)
        {
            Console.BackgroundColor = UiControl.DefaultBackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(" ");
            Console.ForegroundColor = UiControl.DefaultForegroundColor;
            drawable.IsDrawn = false;
            drawable.Erase = false;
        }
    }
}