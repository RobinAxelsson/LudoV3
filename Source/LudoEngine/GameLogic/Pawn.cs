using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using System;
using System.Linq;

namespace LudoEngine.Models
{
    public class Pawn
    {
        private static int highestId { get; set; }
        static Pawn() => highestId = 0;
        public int Id { get; } //Game Id not SQL
        public Pawn(TeamColor color)
        {
            Color = color;
            Id = highestId;
            highestId++;
        }
        public static event Action<Pawn> OnBounceEvent;
        public static event Action<Pawn, int> OnGoalEvent;
        public static event Action<Pawn> OnAllTeamPawnsOutEvent;
        public static event Action<Pawn, TeamColor, int> OnEradicationEvent;
        public static event Action<TeamColor> GameLoserEvent;
        public static event Action GameOverEvent;
        public static event Action<Pawn> OnSafeZoneEvent;
        public bool IsSelected { get; set; }
        public TeamColor Color { get; set; }
        public IGameSquare CurrentSquare() => Board.BoardSquares.Find(x => x.Pawns.Contains(this));
        public bool Based() => Board.PawnsInBase(Color).Contains(this); //Kolla om Pawn ligger i basen
        public void Move(int dice)
        {
            var tempSquare = CurrentSquare();
            bool startingSquareIsSafeZoneSquare = tempSquare is SafezoneSquare;
            tempSquare.Pawns.Remove(this);

            bool lastIteration;
            bool bounced = false;

            for (var i = 0; i < dice; i++)
            {
                lastIteration = i == dice - 1;

                if (tempSquare is GoalSquare || bounced == true)
                {
                    tempSquare = Board.GetBack(Board.BoardSquares, tempSquare, Color);
                    bounced = true;
                }
                else
                {
                    tempSquare = Board.GetNext(Board.BoardSquares, tempSquare, Color);
                }
                if (lastIteration == true && tempSquare is GoalSquare)
                {
                    this.IsSelected = false;

                    if (Board.GetTeamPawns(Color).Count == 0)
                        OnAllTeamPawnsOutEvent?.Invoke(this);
                    else
                        OnGoalEvent?.Invoke(this, Board.GetTeamPawns(Color).Count);

                    bool onlyOneTeamLeft = Board.AllPlayingPawns().Select(x => x.Color).ToList().Count == 1;
                    if (onlyOneTeamLeft)
                    {
                        GameLoserEvent?.Invoke(Board.AllPlayingPawns().Select(x => x.Color).ToList()[0]);
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
                var eradicateBase = Board.BaseSquare((TeamColor)enemyColor);
                eradicateBase.Pawns.AddRange(tempSquare.Pawns);
                tempSquare.Pawns.Clear();
            }

            if(pawnsToEradicate != 0) OnEradicationEvent?.Invoke(this, (TeamColor)enemyColor, pawnsToEradicate);
            tempSquare.Pawns.Add(this);
            if (bounced == true) OnBounceEvent?.Invoke(this);
            if (tempSquare is SafezoneSquare && startingSquareIsSafeZoneSquare == false) OnSafeZoneEvent?.Invoke(this);

            this.IsSelected = false;
        }
    }
}