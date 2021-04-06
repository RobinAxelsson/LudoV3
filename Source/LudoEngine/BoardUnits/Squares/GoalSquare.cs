﻿using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;
using System.Collections.Generic;

namespace LudoEngine.BoardUnits.Main
{
    public class GoalSquare : IGameSquare
    {
        public GoalSquare(int boardX, int boardY)
        {
            BoardX = boardX;
            BoardY = boardY;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public TeamColor? Color { get; set; } = null;
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();
        public BoardDirection DefaultDirection { get; set; }
        public BoardDirection DirectionNext(TeamColor Color)
        {
                return
                   Color == TeamColor.Yellow ? BoardDirection.Down :
                   Color == TeamColor.Blue ? BoardDirection.Left :
                   Color == TeamColor.Red ? BoardDirection.Up : BoardDirection.Right;
        }
    }
}