using System;
using System.Collections.Generic;
using System.IO;
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
        private StreamWriter Logger;
        private string LoggerMessage = "";
        public Stephan(TeamColor color)
        {
            StephanColor = color;
            int number = 0;
            if (!Directory.Exists(Environment.CurrentDirectory + @"\StephanLogs")) Directory.CreateDirectory(Environment.CurrentDirectory + @"\StephanLogs");
            foreach (FileInfo finf in new DirectoryInfo(Environment.CurrentDirectory + @"\StephanLogs").GetFiles())
            {
                if (finf.Name.StartsWith($"stephan_{StephanColor.ToString()}") && finf.Extension == ".log")
                {
                    number++;
                }
            }
            Logger = new StreamWriter($@"{Environment.CurrentDirectory}\StephanLogs\stephan_{StephanColor.ToString()}{number.ToString()}.log");
            LoggerMessage = $"{DateTime.Now.ToShortTimeString()}: Initializing Stephan. Color: {StephanColor}";
            WriteLogging(LoggerMessage);
            LoggerMessage = "";

            StephanPawns = new List<Pawn>();

        }
        public (Pawn PlayPawn, bool TakeOutTwo) Play(int rolled)
        {
            LoggerMessage = $"\n\n[Method: Play] New instance\n\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Rolled: {rolled}";
            playerPawns = Board.PawnsOnBoard();
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
                        if (Board.StartSquare(StephanColor).Pawns.FindAll(x => x.Color == StephanColor).Count == 0)
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                            WriteLogging(LoggerMessage);
                            return (null, true); //Ta ut två pjäser!
                        }
                        else
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                            WriteLogging(LoggerMessage);
                            return (CalculateWhatPieceToMove(StephanPawns, rolled), false);
                        }
                    }

                    else if (CalcInfo.takeoutCount == 1)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Making sure no pawn is at spawn location";
                        if (Board.StartSquare(StephanColor).Pawns.FindAll(x => x.Color == StephanColor).Count == 0)
                        {
                            foreach (var pawn in Board.PawnsInBase(StephanColor))
                            {
                                StephanPawns.Add(pawn);
                                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                                WriteLogging(LoggerMessage);
                                return (pawn, false);
                            }
                        }
                        else
                        {
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                            WriteLogging(LoggerMessage);
                            return (CalculateWhatPieceToMove(StephanPawns, rolled), false);

                        }
                    }
                }
                else if (rolled == 1)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Making sure no pawn is at spawn location";
                    if (Board.StartSquare(StephanColor).Pawns.FindAll(x => x.Color == StephanColor).Count == 0)
                    {
                        foreach (var pawn in Board.PawnsInBase(StephanColor))
                        {
                            StephanPawns.Add(pawn);
                            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] No pawn found";
                            WriteLogging(LoggerMessage);
                            return (pawn, false);
                        }
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Piece found. Doing move calculations instead";
                        WriteLogging(LoggerMessage);
                        return (CalculateWhatPieceToMove(StephanPawns, rolled), false);

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
            var squarePosition = Board.StartSquare(StephanColor); //Start at spawn
            foreach (var pawn in StephanPawns)
            {
                squarePosition = pawn.CurrentSquare();
                for (var i = 0; i <= dice - 1; i++)
                {
                    squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, StephanColor);
                }
                if (squarePosition is StartSquare && squarePosition.Color != StephanColor)
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
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {StephanPawns.Count.ToString()}";
            if (StephanPawns.Count > 0)
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
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 6\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {StephanPawns.Count.ToString()}";
                    if (StephanPawns.Count <= 2)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is less than two. Will attempt to pull out two pawns";
                        return (null, false, true, 2); //Tar ut 2
                    }
                    else if (StephanPawns.Count == 3)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to three. Will attempt to pull out one pawn";
                        return (null, false, true, 1); //Tar ut 1
                        //spela igen
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to four. Will now calculate most appropriate piece to move.";
                        return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
                    }
                }
                else if (dice == 1)
                {

                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a 1\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {StephanPawns.Count.ToString()}";
                    if (StephanPawns.Count < 4)
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is less than four. Will attempt to pull out one pawn";
                        return (null, false, true, 1); //Tar ut 1
                    }
                    else
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to four. Will now calculate most appropriate piece to move.";
                        return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
                    }
                }
                else
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Dice resulted in a {dice}\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Will now calculate most appropriate piece to move.";
                    return (CalculateWhatPieceToMove(StephanPawns, dice), false, false, 0); //Returnar en pawn
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
                    SquarePositionCalc = Board.GetNext(Board.BoardSquares, SquarePositionCalc, StephanColor);
                    if (SquarePositionCalc.GetType() == typeof(GoalSquare))
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Can reach a Goal-square. Returning move";
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == StephanColor);
                    }
                    if (SquarePositionCalc.GetType() == typeof(SafezoneSquare))
                    {
                        LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Can reach a Safezone-square. Returning move";
                        return SquarePosition.Pawns.Find(pawn => pawn.Color == StephanColor);
                    }
                }
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Checking if a pawn has the possibility to end up in enemy start square";
            var WillIEndUpInBase = MakeSureNotEndingUpInEnemyStartSquare(dice);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Result: {WillIEndUpInBase.WillEndUpInEnemyStartSquare}";
            var pawnInTakeOut = TakeOutSquare.Pawns.Find(pawn => pawn.Color == StephanColor);
            if (pawnInTakeOut != null)
            {
                return pawnInTakeOut;
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculateWhatPieceToMove] Calculating distance between min and max pawn";
            //Calculate pawn distance
            int distance = 0;
            Pawn closestPawn = null;
            Pawn farthestPawn = GetFarthestPawn();
            var squarePosition = Board.StartSquare(StephanColor); //Start at spawn
            int furthestIndex = 0;
            for (var i = 0; i <= Board.TeamPath(StephanColor).Count; i++)
            {
                squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, StephanColor);
                if (squarePosition.Pawns.Contains(farthestPawn))
                {
                    furthestIndex = i;
                }
            }
            squarePosition = Board.StartSquare(StephanColor); //Start at spawn
            for (var i = 0; i <= furthestIndex; i++)
            {
                squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, StephanColor);
                if (closestPawn != null)
                {
                    distance++;
                }
                if (squarePosition.Pawns.Find(p => p.Color == StephanColor) != null && closestPawn == null)
                {
                    closestPawn = squarePosition.Pawns.Find(p => p.Color == StephanColor);
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
        private (bool CanEradicate, Pawn PawnToEradicateWith) CheckForPossibleEradication(int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Calculating possible eradication";
            var eradication = false;
            var eradicationPawn = new Pawn(StephanColor);
            var squarePosition = Board.StartSquare(StephanColor); //Start at spawn
            foreach (var pawn in StephanPawns)
            {
                squarePosition = pawn.CurrentSquare();
                for (var i = 0; i <= dice - 1; i++)
                {
                    squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, StephanColor);
                }
                if (squarePosition.Pawns.Find(p => p.Color != StephanColor) != null)
                {
                    eradication = true;
                    eradicationPawn = pawn;
                }
            }
            return (eradication, eradicationPawn);
        }
        private void WriteLogging(string input)
        {
            Logger.Write(input);
            Logger.WriteLine("");
            Logger.Flush();
        }
    }
}