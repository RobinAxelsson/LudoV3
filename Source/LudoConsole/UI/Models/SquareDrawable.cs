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
            var charCoords = ReadCharCoords();

            foreach (var charCoord in charCoords)
            {
                Memory.Add(new LudoDrawable(charCoord.chr, charCoord.coords, TranslateColor((TeamColor)Square.Color)));
            }
        }
        public IGameSquare Square { get; set; }
        public List<IDrawable> UpdatePawns()
        {
            var drawList = new List<IDrawable>();
            var pawns = Square.Pawns;
            for (int i = 0; i < pawns.Count; i++)
            {
                var toClone = Memory[i];
                var pawnDrawable = new PawnDrawable(toClone.CoordinateX, toClone.CoordinateY, toClone.BackgroundColor, TranslateColor(pawns[i].Color));
                drawList.Add(pawnDrawable);
            }
            return drawList;
        }
        public List<IDrawable> Memory { get; set; } = new List<IDrawable>();
        private (int X, int Y) TranslateCoords((int X, int Y) coords) => (Square.BoardX + coords.X, Square.BoardY + coords.Y);
        private ConsoleColor TranslateColor(TeamColor color) =>
            color == TeamColor.Blue ? ConsoleColor.DarkBlue :
            color == TeamColor.Green ? ConsoleColor.Green :
            color == TeamColor.Red ? ConsoleColor.Red :
            color == TeamColor.Yellow ? ConsoleColor.Yellow : ConsoleColor.White;
        private List<(char chr, (int X, int Y) coords)> ReadCharCoords(string filePath = _filepath)
        {
            var charCoord = new List<(char chr, (int X, int Y))>();
            string[] lines = File.ReadAllLines(filePath);

            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                foreach (char chr in line)
                {
                        charCoord.Add((chr, TranslateCoords((x, y))));
                    x++;
                }
                y++;
                x = 0;
            }
            return charCoord;
        }
    }
}
