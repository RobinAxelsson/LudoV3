using LudoConsole.UI.Interfaces;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole.UI.Models
{
    public class SquareDrawable : IDrawableSquare
    {
        private const string _filepath = @"UI/Map/square.txt";
        public SquareDrawable(IGameSquare square)
        {
            Square = square;
            var charCoords = ReadCharCoords();

            foreach (var charCoord in charCoords)
            {
                var color = Square.Color != null ? TranslateColor((TeamColor)Square.Color) : ConsoleColor.White;
                Memory.Add(new LudoDrawable(charCoord.chr, charCoord.coords, color));
            }
        }
        private List<(char chr, (int X, int Y) coords)> ReadCharCoords(string filePath = _filepath)
        {
            var charCoords = new List<(char chr, (int X, int Y) coords)>();
            string[] lines = File.ReadAllLines(filePath);

            int xMax = lines.ToList().Select(x => x.Length).Max();
            int yMax = lines.Length;

            (int X, int Y) trueUpLeft = (xMax * Square.BoardX, yMax * Square.BoardY);
            TrueUpLeft = trueUpLeft;

            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                foreach (char chr in line)
                {
                    charCoords.Add((chr, (trueUpLeft.X + x, trueUpLeft.Y + y)));
                    x++;
                }
                y++;
                x = 0;
            }
            return charCoords;
        }
        private (int X, int Y) TrueUpLeft { get; set; }
        public IGameSquare Square { get; set; }
        public List<IDrawable> DrawPawns()
        {
            var drawList = new List<IDrawable>();
            var pawns = Square.Pawns;
            for (int i = 0; i < pawns.Count; i++)
            {
                var toClone = Memory[i];
                var pawnDrawable = new PawnDrawable(toClone.CoordinateX, toClone.CoordinateY, TranslateColor(pawns[i].Color), toClone.BackgroundColor);
                drawList.Add(pawnDrawable);
            }
            return drawList;
        }
        public List<IDrawable> Memory { get; set; } = new List<IDrawable>();
        private ConsoleColor TranslateColor(TeamColor color) =>
            color == TeamColor.Blue ? ConsoleColor.DarkBlue :
            color == TeamColor.Green ? ConsoleColor.Green :
            color == TeamColor.Red ? ConsoleColor.Red :
            color == TeamColor.Yellow ? ConsoleColor.Yellow : ConsoleColor.White;

        public static List<SquareDrawable> ConvertAllSquares(List<IGameSquare> squares) => squares.Select(x => new SquareDrawable(x)).ToList();
    }
}
