﻿using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Interfaces;

namespace LudoEngine.Board.Square
{
    internal sealed class SquareSafeZone : SquareBase
    {
        public SquareSafeZone(int boardX, int boardY, TeamColor color, BoardDirection direction)
        {
            Color = color;
            BoardX = boardX;
            BoardY = boardY;
            DefaultDirection = direction;
        }
        public override BoardDirection DirectionNext(TeamColor color) => DefaultDirection;
    }
}