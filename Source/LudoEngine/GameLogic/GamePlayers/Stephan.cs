using System;
using System.Collections.Generic;
using System.Threading;
using LudoConsole.Main;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;

namespace LudoEngine.GameLogic.GamePlayers
{
    public class Stephan : IGamePlayer
    {
        public TeamColor Color { get; set; }
        public List<Pawn> Pawns { get; set; }
        private Action<TeamColor, int> DisplayDice { get; set; }
        private Action<string> WriteLogging { get; set; }
        private string LoggerMessage { get; set; } = "";
        public Stephan(TeamColor color, Action<TeamColor, int> displayDice, ILog log = null)
        {
            Color = color;
            DisplayDice = displayDice;

            if (log != null)
                WriteLogging = log.Log;
            else
                WriteLogging = x => x = "";

            LoggerMessage = $"{DateTime.Now.ToShortTimeString()}: Initializing Stephan. Color: {Color}";
            WriteLogging(LoggerMessage);
            LoggerMessage = "";

            Pawns = Board.GetTeamPawns(color);
        }
        public void Play(IDice dice)
        {
            int diceRoll = dice.Roll();
            
            if(DisplayDice != null)
            DisplayDice(Color, diceRoll);


            var selectablePawns = GameRules.SelectablePawns(Color, diceRoll);
            if (selectablePawns.Count == 0) return;

            var result = play(diceRoll);

            if (result.TakeOutTwo == true)
            {
                var basePawns = Board.PawnsInBase(Color);
                basePawns[0].Move(1);
                basePawns[0].Move(1);
                return;
            }
            if (result.TakeOutTwo == false && result.PlayPawn != null)
            {
                result.PlayPawn.Move(diceRoll);
            }

            if (diceRoll == 6 && result.TakeOutTwo == false) Play(dice);

            return;
        }
        private (Pawn PlayPawn, bool TakeOutTwo) play(int rolled)
        {
            LoggerMessage = $"\n\n[Method: Play] New instance\n\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Rolled: {rolled}";

            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Calculating play...";
            var CalcInfo = CalculatePlay(rolled);

            if (CalcInfo.pawnToMove != null && !CalcInfo.pass && !CalcInfo.takeout)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Will move pawn";
                if (rolled == 6)
                {
                    WriteLogging(LoggerMessage);
                    return (CalcInfo.pawnToMove, false);
                }
                else
                {
                    WriteLogging(LoggerMessage);
                    return (CalcInfo.pawnToMove, false);
                }

            }
            else if (CalcInfo.takeout)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Will take out pawn(s)";
                if (rolled == 6)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Making sure no pawn is at spawn location";

                    if (CalcInfo.takeoutCount == 2)
                    {
                        var pawnsInStartSquare = Board.StartSquare(Color).Pawns;
                        if (pawnsInStartSquare.Count == 0)
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                            WriteLogging(LoggerMessage);
                            return (null, true); //Ta ut två pjäser!
                        }
                        else
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                            WriteLogging(LoggerMessage);
                            return (CalculateWhatPieceToMove(Pawns, rolled), false);
                        }
                    }

                    else if (CalcInfo.takeoutCount == 1)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Making sure no pawn is at spawn location";
                        if (Board.StartSquare(Color).Pawns.FindAll(x => x.Color == Color).Count == 0)
                        {
                            foreach (var pawn in Board.PawnsInBase(Color))
                            {
                                Pawns.Add(pawn);
                                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                                WriteLogging(LoggerMessage);
                                return (pawn, false);
                            }
                        }
                        else
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                            WriteLogging(LoggerMessage);
                            return (CalculateWhatPieceToMove(Pawns, rolled), false);

                        }
                    }
                }
                else if (rolled == 1)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Making sure no pawn is at spawn location";
                    if (Board.StartSquare(Color).Pawns.FindAll(x => x.Color == Color).Count == 0)
                    {
                        foreach (var pawn in Board.PawnsInBase(Color))
                        {
                            Pawns.Add(pawn);
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                            WriteLogging(LoggerMessage);
                            return (pawn, false);
                        }
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                        WriteLogging(LoggerMessage);
                        return (CalculateWhatPieceToMove(Pawns, rolled), false);

                    }
                }
            }
            else if (CalcInfo.pass)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Passing round";
                WriteLogging(LoggerMessage);
                return (null, false);
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Returning null, arrived at bottom of method. This should not happen.";
            WriteLogging(LoggerMessage);
            return (null, false);
        }

        private (bool WillEndUpInEnemyStartSquare, List<Pawn> pawnsToNotMove) MakeSureNotEndingUpInEnemyStartSquare(int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: MakeSureNotEndingUpInEnemyStartSquare] Doing calculations to see what friendly pawns can end up in enemy spawn square";
            bool Result = false;
            List<Pawn> PawnsNotToMove = new List<Pawn>();
            var squarePosition = Board.StartSquare(Color); //Start at spawn
            foreach (var pawn in Pawns)
            {
                squarePosition = pawn.CurrentSquare();
                for (var i = 0; i <= dice - 1; i++)
                {
                    squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, Color);
                }
                if (squarePosition is StartSquare && squarePosition.Color != Color)
                {
                    Result = true;
                    PawnsNotToMove.Add(pawn);
                }
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: MakeSureNotEndingUpInEnemyStartSquare] Result: {PawnsNotToMove.Count}";
            return (Result, PawnsNotToMove);
        }
        private (Pawn pawnToMove, bool pass, bool takeout, int takeoutCount) CalculatePlay(int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {Pawns.Count.ToString()}";
            if (Pawns.Count > 0)
            {

                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is more than 0";
                var WhatAmINotGonnaMove = MakeSureNotEndingUpInEnemyStartSquare(dice);
                var eradicationInfo = CheckForPossibleEradication(dice);
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Eradication status: {eradicationInfo.CanEradicate}";
                if (eradicationInfo.CanEradicate)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Can eradicate. Checking if eradication will result in enemy spawn position";
                    if (!WhatAmINotGonnaMove.pawnsToNotMove.Contains(eradicationInfo.PawnToEradicateWith))
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Eradication will not result in enemy spawn position. Returning move";
                        return (eradicationInfo.PawnToEradicateWith, false, false, 0); //Returnar en pawn. Han vet att han kommer slå ut en annan
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Eradication will result in enemy spawn position. Aborting move and continuing calculation";
                    }
                }

                if (dice == 6)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 6\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {Pawns.Count.ToString()}";
                    if (Board.OutOfBasePawns(Color).Count < 3)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is less than three. Will attempt to pull out two pawns";
                        return (null, false, true, 2); //Take out two
                    }
                    else if (Board.OutOfBasePawns(Color).Count == 3)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to three. Will attempt to pull out one pawn";
                        return (null, false, true, 1); //take out one, play again
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to four. Will now calculate most appropriate piece to move.";
                        return (CalculateWhatPieceToMove(Pawns, dice), false, false, 0);
                    }
                }
                else if (dice == 1)
                {

                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 1\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {Pawns.Count.ToString()}";
                    if (Pawns.Count < 4)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is less than four. Will attempt to pull out one pawn";
                        return (null, false, true, 1); //Tar ut 1
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to four. Will now calculate most appropriate piece to move.";
                        return (CalculateWhatPieceToMove(Pawns, dice), false, false, 0); //Returnar en pawn
                    }
                }
                else
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a {dice}\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Will now calculate most appropriate piece to move.";
                    return (CalculateWhatPieceToMove(Pawns, dice), false, false, 0); //Returnar en pawn
                }
            }
            else
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to 0";
                if (dice == 6)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 6\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Pulling out two pawns";
                    return (null, false, true, 2); //Tar ut 2
                }
                else if (dice == 1)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 1\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Pulling out one pawn";
                    return (null, false, true, 1); //Tar ut 1
                }
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a {dice}\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Cannot make a move.";
            return (null, true, false, 0); //Passar tur
        }
        private Pawn CalculateWhatPieceToMove(List<Pawn> piecesOut, int dice)
        {

            if (piecesOut == null) throw new ArgumentNullException(nameof(piecesOut));
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Looping through each friendly pawn on board";

            foreach (var piece in piecesOut)
            {
                var SquarePosition = Board.BoardSquares.Find(square => square == piece.CurrentSquare());
                var SquarePositionCalc = SquarePosition;
                for (var i = 0; i <= dice; i++)
                {
                    SquarePositionCalc = Board.GetNext(Board.BoardSquares, SquarePositionCalc, Color);
                    if (SquarePositionCalc.GetType() == typeof(GoalSquare))
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Can reach a Goal-square. Returning move";
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == Color);
                    }
                    if (SquarePositionCalc.GetType() == typeof(SafezoneSquare))
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Can reach a Safezone-square. Returning move";
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == Color);
                    }
                }
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Checking if a pawn has the possibility to end up in enemy start square";
            var WillIEndUpInBase = MakeSureNotEndingUpInEnemyStartSquare(dice);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Result: {WillIEndUpInBase.WillEndUpInEnemyStartSquare}";
            var pawnInTakeOut = Board.StartSquare(Color).Pawns.Find(pawn => pawn.Color == Color);
            if (pawnInTakeOut != null)
            {
                return pawnInTakeOut;
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Calculating distance between min and max pawn";
            //Calculate pawn distance
            int distance = 0;
            Pawn closestPawn = null;
            Pawn farthestPawn = GetFarthestPawn();
            var squarePosition = Board.StartSquare(Color); //Start at spawn
            int furthestIndex = 0;
            for (var i = 0; i <= Board.TeamPath(Color).Count; i++)
            {
                squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, Color);
                if (squarePosition.Pawns.Contains(farthestPawn))
                {
                    furthestIndex = i;
                }
            }
            squarePosition = Board.StartSquare(Color); //Start at spawn
            for (var i = 0; i <= furthestIndex; i++)
            {
                squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, Color);
                if (closestPawn != null)
                {
                    distance++;
                }
                if (squarePosition.Pawns.Find(p => p.Color == Color) != null && closestPawn == null)
                {
                    closestPawn = squarePosition.Pawns.Find(p => p.Color == Color);
                }
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Distance is: {distance.ToString()} squares";
            if (distance >= 10 && !WillIEndUpInBase.pawnsToNotMove.Contains(closestPawn))
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Result is above 10 and closest pawn will not end up in enemy spawn square. Will move closest pawn";
                return closestPawn;
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Result is below 10. Moving farthest pawn.";
            return GetFarthestPawn();
        }

        private Pawn GetFarthestPawn()
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: GetFarthestPawn] Calculating farthest pawn";
            Pawn pawn = null;
            foreach (var square in Board.TeamPath(Color))
            {
                if (square.Pawns.Count > 0 && 
                    square.Pawns[0].Color == Color &&
                    square.GetType() != typeof(GoalSquare)) 
                    pawn = square.Pawns[0];
            }
            return pawn;
        }
        private (bool CanEradicate, Pawn PawnToEradicateWith) CheckForPossibleEradication(int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Calculating possible eradication";
            var eradication = false;
            var eradicationPawn = new Pawn(Color);
            var squarePosition = Board.StartSquare(Color); //Start at spawn
            foreach (var pawn in Pawns)
            {
                squarePosition = pawn.CurrentSquare();
                for (var i = 0; i <= dice - 1; i++)
                {
                    squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, Color);
                }
                if (squarePosition.Pawns.Find(p => p.Color != Color) != null)
                {
                    eradication = true;
                    eradicationPawn = pawn;
                }
            }
            return (eradication, eradicationPawn);
        }

    }
}