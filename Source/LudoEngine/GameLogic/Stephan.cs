using System;
using System.Collections.Generic;
using LudoEngine.Board.Classes;
using LudoEngine.Board.Intefaces;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoEngine.GameLogic
{
    public class Stephan
    {
        public TeamColor StephanColor { get; set; }
        public IGameSquare TakeOutSquare { get; set; }
        public IGameSquare FarTakeOutSquare { get; set; }
        public List<Pawn> StephanPawnsOut { get; set; }
        public Stephan()
        {
            if (StephanColor == null)
            {
                throw new Exception("StephanColor is null!");
            }
        }
        public void OnPlayTurn()
        {
            
        }

        public void Play()
        {
    
        }
        
        private string CalculatePlay(int dice)
        {
            string command = "";
            if (StephanPawnsOut.Count > 0)
            {
                foreach (var piece in StephanPawnsOut)
                {
                    if (CheckForPossibleEradication(piece, dice).CanEradicate)
                    {
                        //Move eradicationPawn!
                    }
                }
                if (dice == 6)
                {
                    if (StephanPawnsOut.Count <= 2)
                    {
                        //Returnera ta ut 2 stycken!
                    }
                    else if(StephanPawnsOut.Count == 3)
                    {
                        //Returnera ta ut en
                    }
                    else
                    {
                        //PawnToMove = CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                        CalculateWhatPieceToMove(StephanPawnsOut, dice);
                        //Returnera move this pawn!
                    }
                }
                else if (dice == 1)
                {
                    if (StephanPawnsOut.Count < 4)
                    {
                        //Returnera ta ut en
                    }
                    else
                    {
                        //PawnToMove = CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                        CalculateWhatPieceToMove(StephanPawnsOut, dice);
                        //Returnera gå fram med tempMovePiece
                    }
                }
                else
                {
                    if (StephanPawnsOut.Count > 0)
                    {
                        //Returnera gå fram
                    }
                    else
                    {
                        //Returnera stå kvar ett kast
                    }
                }
            }
            else
            {
                if (dice == 6)
                {
                    if (StephanPawnsOut.Count <= 2)
                    {
                        //Returnera ta ut 2 stycken!
                    }
                    else if(StephanPawnsOut.Count == 3)
                    {
                        //Returnera ta ut en
                    }
                }
                else if (dice == 1)
                {
                    //Returnera ta ut en
                }
                else
                {
                    //Returnera stå över ett kast
                }
            }
        }

        private Pawn CalculateWhatPieceToMove(List<Pawn> PiecesOut, int dice)
        {
            var gameSquares = new List<IGameSquare>();
            foreach (var piece in PiecesOut)
            {
                var SquarePosition = gameSquares.Find(square =>
                    piece.PositionX == square.BoardX && piece.PositionY == square.BoardY);
                for (var i = 0; i <= dice; i++)
                {
                    SquarePosition = BoardUtils.GetNext(gameSquares, SquarePosition, StephanColor);
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
            var gameSquares = new List<IGameSquare>();
            var gameSquare = TakeOutSquare;
            var pawn = gameSquare.Pawns.Find(p => p.Color == StephanColor);
            //1 lap = 56 coords, 54 = 2 behind to get last square before gate
            for (var i = 0; i <= 54; i++)
            {
                gameSquare = BoardUtils.GetNext(gameSquares, gameSquare, StephanColor);
                var tempPawn = gameSquare.Pawns.Find(p => p.Color == StephanColor);
                if (tempPawn != null)
                {
                    pawn = tempPawn;
                }
            }
            return pawn;
        }
        private (bool CanEradicate, Pawn PawnToEradicateWith) CheckForPossibleEradication(Pawn inputPawn, int dice)
        {
            var eradication = false;
            var eradicationPawn = new Pawn();
            var playerPawns = new Pawn[16];
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
                                friendlyPawn.PositionX == square.BoardX && friendlyPawn.PositionY == square.BoardY);
                            for (var i = 0; i <= dice; i++)
                            {
                                SquarePosition = BoardUtils.GetNext(gameSquares, SquarePosition, StephanColor);
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