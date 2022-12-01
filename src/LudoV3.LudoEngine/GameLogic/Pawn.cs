using System;
using System.Linq;
using LudoEngine.Board;
using LudoEngine.Board.Square;
using LudoEngine.Enums;

namespace LudoEngine.GameLogic
{
    internal class Pawn
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

        public GameSquareBase CurrentSquare() => GameBoard.BoardSquares.Find(x => x.Pawns.Contains(this));
        public bool Based() => GameBoard.PawnsInBase(GameBoard.BoardSquares, Color).Contains(this);
        public void Move(int dice)
        {
            IsSelected = false;
            var landingSquare = GetLandingSquare(dice);

            if (EnemiesExists(landingSquare))
                EradicateEnemies(landingSquare);

            if (landingSquare is GameSquareGoal)
            {
                ScorePawn();
                return;
            }
            
            CurrentSquare().Pawns.Remove(this);
            landingSquare.Pawns.Add(this);
        }

        private void ScorePawn()
        {
            CurrentSquare().Pawns.Remove(this);

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
        }

        private GameSquareBase GetLandingSquare(int dice)
        {
            var tempSquare = CurrentSquare();

            var bounced = false;
            for (var i = 0; i < dice; i++)
            {
                if (tempSquare is GameSquareGoal || bounced)
                {
                    tempSquare = GameBoard.GetBack(GameBoard.BoardSquares, tempSquare, Color);
                    bounced = true;
                    continue;
                }

                tempSquare = GameBoard.GetNext(GameBoard.BoardSquares, tempSquare, Color);
            }

            return tempSquare;
        }

        private void EradicateEnemies(GameSquareBase tempGameSquare)
        {
            var enemies = tempGameSquare.Pawns.ToArray();
            var enemyColor = tempGameSquare.Pawns[0].Color;
            var eradicateBase = GameBoard.BaseSquare(GameBoard.BoardSquares, enemyColor);
            eradicateBase.Pawns.AddRange(tempGameSquare.Pawns);
            tempGameSquare.Pawns.Clear();

            OnEradicationEvent?.Invoke(this, enemies.ToArray()[0].Color, enemies.Count());
        }

        private bool EnemiesExists(GameSquareBase tempGameSquare)
        {
            return tempGameSquare.Pawns.Any() && tempGameSquare.Pawns[0].Color != Color;
        }
    }
}