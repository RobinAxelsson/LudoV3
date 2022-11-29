﻿using System;
using System.Linq;
using LudoEngine.Board;
using LudoEngine.Board.Square;
using LudoEngine.Enum;
using LudoEngine.Interfaces;

namespace LudoEngine.GameLogic
{
    public class Pawn
    {
        private static int highestId { get; set; }
        public Pawn(TeamColor color)
        {
            Color = color;
            Id = highestId;
            highestId++;
        }
        static Pawn() => highestId = 0;
        public int Id { get; } //Game Id not SQL
        public bool IsSelected { get; set; }
        public TeamColor Color { get; set; }

        public static event Action<Pawn> OnBounceEvent;
        public static event Action<Pawn, int> OnGoalEvent;
        public static event Action<Pawn> OnAllTeamPawnsOutEvent;
        public static event Action<Pawn, TeamColor, int> OnEradicationEvent;
        public static event Action<TeamColor> GameLoserEvent;
        public static event Action GameOverEvent;
        public static event Action<Pawn> OnSafeZoneEvent;

        public IGameSquare CurrentSquare() => GameBoard.BoardSquares.Find(x => x.Pawns.Contains(this));
        public bool Based() => GameBoard.PawnsInBase(GameBoard.BoardSquares, Color).Contains(this);
        public void Move(int dice)
        {
            var tempSquare = CurrentSquare();
            bool startingSquareIsSafeZoneSquare = tempSquare is SquareSafeZone;
            tempSquare.Pawns.Remove(this);

            bool lastIteration;
            bool bounced = false;

            for (var i = 0; i < dice; i++)
            {
                lastIteration = i == dice - 1;

                if (tempSquare is SquareGoal || bounced == true)
                {
                    tempSquare = GameBoard.GetBack(GameBoard.BoardSquares, tempSquare, Color);
                    bounced = true;
                }
                else
                {
                    tempSquare = GameBoard.GetNext(GameBoard.BoardSquares, tempSquare, Color);
                }
                if (lastIteration == true && tempSquare is SquareGoal)
                {
                    this.IsSelected = false;

                    if (GameBoard.GetTeamPawns(GameBoard.BoardSquares, Color).Count == 0)
                        OnAllTeamPawnsOutEvent?.Invoke(this);
                    else
                        OnGoalEvent?.Invoke(this, GameBoard.GetTeamPawns(GameBoard.BoardSquares, Color).Count);

                    bool onlyOneTeamLeft = GameBoard.AllPlayingPawns(GameBoard.BoardSquares).Select(x => x.Color).ToList().Count == 1;
                    if (onlyOneTeamLeft)
                    {
                        GameLoserEvent?.Invoke(GameBoard.AllPlayingPawns(GameBoard.BoardSquares).Select(x => x.Color).ToList()[0]);
                        GameOverEvent?.Invoke();
                    }
                    return;
                }
            }

            TeamColor? enemyColor = null;
            int pawnsToEradicate = 0;
            if (tempSquare.Pawns.Count != 0 && tempSquare.Pawns[0].Color != Color)
            {
                enemyColor = tempSquare.Pawns[0].Color;
                pawnsToEradicate = tempSquare.Pawns.Count;
                var eradicateBase = GameBoard.BaseSquare(GameBoard.BoardSquares, (TeamColor)enemyColor);
                eradicateBase.Pawns.AddRange(tempSquare.Pawns);
                tempSquare.Pawns.Clear();
            }

            if(pawnsToEradicate != 0) OnEradicationEvent?.Invoke(this, (TeamColor)enemyColor, pawnsToEradicate);
            tempSquare.Pawns.Add(this);
            if (bounced == true) OnBounceEvent?.Invoke(this);
            if (tempSquare is SquareSafeZone && startingSquareIsSafeZoneSquare == false) OnSafeZoneEvent?.Invoke(this);

            this.IsSelected = false;
        }
    }
}