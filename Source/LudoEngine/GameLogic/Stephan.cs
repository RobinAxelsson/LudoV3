using System;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoEngine.GameLogic
{
    public class Stephan
    {
        public TeamColor StephanColor { get; set; }

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
        
        private void CalculatePlay(int dice)
        {

            var PiecesOut = new Pawn[3];
            var StartPosition = 0; //Startposition där du tar ut din gubbe
            var GoalPosition = 30; //Position där du är safe
            //Default return = Passera till nästa person
            if (PiecesOut.Length > 0)
            {
                foreach (var piece in PiecesOut)
                {
                    if (GetShortestOpponentDistance(piece, dice) == 0)
                    {
                        //Return slå ut fiende!
                    }
                }
                if (dice == 6)
                {
                    if (PiecesOut.Length <= 2)
                    {
                        //Returnera ta ut 2 stycken!
                    }
                    else if(PiecesOut.Length == 3)
                    {
                        //Returnera ta ut en
                    }
                    else
                    {
                        //PawnToMove = CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                        CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                    }
                }
                else if (dice == 1)
                {
                    if (PiecesOut.Length < 4)
                    {
                        //Returnera ta ut en
                    }
                    else
                    {
                        //PawnToMove = CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                        CalculateWhatPieceToMove(PiecesOut, dice,GoalPosition,StartPosition);
                        //Returnera gå fram med tempMovePiece
                    }
                }
                else
                {
                    if (PiecesOut.Length > 0)
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
                    if (PiecesOut.Length <= 2)
                    {
                        //Returnera ta ut 2 stycken!
                    }
                    else if(PiecesOut.Length == 3)
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

        private Pawn CalculateWhatPieceToMove(Pawn[] PiecesOut, int dice, int GoalPosition, int StartPosition)
        {
            var tempMovePiece = PiecesOut[0]; //Defaultar till 0
            foreach (var piece in PiecesOut)
            {
                if (piece.SquareIndex + dice == GoalPosition)
                {
                    tempMovePiece = piece;
                }
                else if (piece.SquareIndex == StartPosition && tempMovePiece.SquareIndex + dice != GoalPosition)
                {
                    tempMovePiece = piece;
                }
                else
                {
                    if (tempMovePiece.SquareIndex < piece.SquareIndex)
                    {
                        tempMovePiece = piece;
                    }
                }
            }

            return tempMovePiece;
        }
        private int GetShortestOpponentDistance(Pawn inputPawn, int dice)
        {
            var distance = 50;
            var playerPawns = new Pawn[16];
            foreach (var pawn in playerPawns)
            {
                if (pawn.Color != StephanColor)
                {
               
                    if (pawn.SquareIndex > inputPawn.SquareIndex)
                    {
                        var tempDistance = 0;
                        tempDistance = pawn.SquareIndex - (inputPawn.SquareIndex + dice);
                        if(tempDistance < distance) distance = tempDistance;
                    }
                    else
                    {
                        distance = 5000;
                    }
                }
            }

            return distance;
        }
    }
}