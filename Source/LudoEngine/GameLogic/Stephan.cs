using System;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoEngine.GameLogic
{
    public class Stephan
    {
        public TeamColor StephanColor { get; set; }
        public IGameSquare TakeOutSquare { get; set; }
        public IGameSquare FarTakeOutSquare { get; set; }
        public List<Pawn> StephanPawns { get; set; }
        private List<Pawn> playerPawns { get; set; }
        public Stephan()
        {
            StephanPawns = new List<Pawn>();
            playerPawns = Board.PawnsOnBoard();
        }
        public void Play()
        {
            int rolled = ActivePlayer.RollDice();
            var CalcInfo = CalculatePlay(rolled);
            Console.Title = "Stephan rolled: " + rolled;
            if (CalcInfo.pawnToMove != null && !CalcInfo.pass && !CalcInfo.takeout)
            {
                if (rolled == 6)
                {
                    StephanMove(CalcInfo.pawnToMove, rolled);
                    Play(); //Spela igen!
                }
                else
                {
                    StephanMove(CalcInfo.pawnToMove, rolled);
                    ActivePlayer.NextTeam();
                }
               
            }
            else if(CalcInfo.takeout)
            {
                if (rolled == 6)
                {
                    if (CalcInfo.takeoutCount == 2)
                    {
                        for (var i = 0; i <= CalcInfo.takeoutCount; i++)
                        {
                            foreach (var pawn in StephanPawns)
                            {
                                if (pawn.Based())
                                {
                                    TakeOutSquare.Pawns.Add(pawn);
                                    ActivePlayer.NextTeam();
                                }
                            }
                   
                        }
                    }
                    else if (CalcInfo.takeoutCount == 1)
                    {
                        foreach (var pawn in Board.PawnsInBase(StephanColor))
                        {
                            if (TakeOutSquare.Pawns.Count == 0)
                            {
                                TakeOutSquare.Pawns.Add(pawn);
                                StephanPawns.Add(pawn);
                                ActivePlayer.NextTeam();
                            }
                            else
                            {
                                ActivePlayer.NextTeam();
                            }
                        }
                    }
                }
                else if (rolled == 1)
                {
                    foreach (var pawn in Board.PawnsInBase(StephanColor))
                    {
                        if(TakeOutSquare.Pawns.Count == 0)
                        {
                            TakeOutSquare.Pawns.Add(pawn);
                            StephanPawns.Add(pawn);
                            ActivePlayer.NextTeam();
                        }
                       else
                        {
                            ActivePlayer.NextTeam();
                        }
                    } 
                }
            }
            else if (CalcInfo.pass)
            {
                ActivePlayer.NextTeam();
            }

        }

        private void StephanMove(Pawn pawn, int dice)
        {
            pawn.Move(dice);
        }
        private (Pawn pawnToMove, bool pass, bool takeout, int takeoutCount) CalculatePlay(int dice)
        {
            if (StephanPawns.Count > 0)
            {
                foreach (var piece in StephanPawns)
                {
                    var eradicationInfo = CheckForPossibleEradication(piece, dice);
                    if (eradicationInfo.CanEradicate)
                    {
                        return (eradicationInfo.PawnToEradicateWith, false, false, 0); //Returnar en pawn. Han vet att han kommer slå ut en annan

                    }
                }
                if (dice == 6)
                {
                    if (StephanPawns.Count <= 2)
                    {
                        return (null, false, true, 2); //Tar ut 2
                    }
                    else if(StephanPawns.Count == 3)
                    {
                        return (null, false, true, 1); //Tar ut 1
                    }
                    else
                    {
                        return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
                    }
                }
                else if (dice == 1)
                {
                    if (StephanPawns.Count < 4)
                    {
                        return (null, false, true, 1); //Tar ut 1
                    }
                    else
                    {
                        return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
                    }
                }
                else
                {
                    return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
                }
            }
            else
            {
                if (dice == 6)
                {
                    if (StephanPawns.Count <= 2)
                    {
                        return (null, false, true, 2); //Tar ut 2
                    }
                    else if(StephanPawns.Count == 3)
                    {
                        return (null, false, true, 1); //Tar ut 1
                    }
                }
                else if (dice == 1)
                {
                    return (null, false, true, 1); //Tar ut 1
                }
            }
            return (null, true, false, 0); //Passar tur
        }
        private Pawn CalculateWhatPieceToMove(List<Pawn> piecesOut, int dice)
        {
            if (piecesOut == null) throw new ArgumentNullException(nameof(piecesOut));
            foreach (var piece in piecesOut)
            {
                var SquarePosition = Board.BoardSquares.Find(square => square == piece.CurrentSquare());
                for (var i = 0; i <= dice; i++)
                {
                    SquarePosition = Board.GetNext(Board.BoardSquares, SquarePosition, StephanColor);
                    if (SquarePosition.GetType() == typeof(GoalSquare))
                    {
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == StephanColor);
                    }
                    if (SquarePosition.GetType() == typeof(SafezoneSquare))
                    {
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == StephanColor);
                    }
                }
            }
            var pawnInTakeOut = TakeOutSquare.Pawns.Find(pawn => pawn.Color == StephanColor);
            if (pawnInTakeOut != null)
            {
                return pawnInTakeOut;
            }
            return GetFarthestPawn();

        }

        private Pawn GetFarthestPawn()
        {
            var pawn = new Pawn(StephanColor); 
            foreach (var square in Board.TeamPath(StephanColor))
            {
                foreach (var p in square.Pawns)
                {
                    if (p.Color == StephanColor)
                    {
                        pawn = p;
                    }
                }
            }
            return pawn;
        }
        private (bool CanEradicate, Pawn PawnToEradicateWith) CheckForPossibleEradication(Pawn inputPawn, int dice)
        {
            var eradication = false;
            var eradicationPawn = new Pawn(StephanColor);
            foreach (var enemyPawn in playerPawns)
            {
                if (enemyPawn.Color != StephanColor)
                {
                    foreach (var friendlyPawn in playerPawns)
                    {
                        if (friendlyPawn.Color == StephanColor)
                        {
                            var gameSquares = new List<IGameSquare>();
                            var SquarePosition = gameSquares.Find(square =>
                                friendlyPawn.CurrentSquare() == square);
                            for (var i = 0; i <= dice; i++)
                            {
                                SquarePosition = Board.GetNext(gameSquares, SquarePosition, StephanColor);
                            }
                            if (SquarePosition.Pawns.Contains(enemyPawn))
                            {
                                eradicationPawn = friendlyPawn;
                                eradication = true;
                            }
                        }
                    }
                }
            }
            return (eradication, eradicationPawn);
        }
    }
}