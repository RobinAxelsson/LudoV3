using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;
using LudoEngine.Models;

namespace LudoEngine.GameLogic.GamePlayers
{
    public class Stephan : IGamePlayer
    {
        public TeamColor Color { get; set; }
        public static event Action<Stephan, int> StephanThrowEvent;
        private Action<string> WriteLogging { get; set; }
        private string LoggerMessage { get; set; } = "";
        public Stephan(TeamColor color, ILog log = null)
        {
            Color = color;

            if (log != null)
                WriteLogging = log.Log;
            else
                WriteLogging = x => x = "";

            LoggerMessage = $"{DateTime.Now.ToShortTimeString()}: Initializing Stephan. Color: {Color}";
            WriteLogging(LoggerMessage);
            LoggerMessage = "";
        }

        public void Play(IDice dice)
        {
            var diceRoll = dice.Roll();
            LoggerMessage = $"\n\n[Method: Play] New instance\n\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Rolled: {diceRoll}";
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Calculating play...";
            StephanThrowEvent?.Invoke(this, diceRoll);
            var result = CalculatePlay(diceRoll);
            if (result.takeout)
            {
                if (result.takeoutCount == 2)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Calculations finished. Committing move";
                    WriteLogging(LoggerMessage);
                    var basePawns = Board.PawnsInBase(Color);
                    basePawns[0].Move(1);
                    basePawns[0].Move(1);

                }
                else if (result.takeoutCount == 1)
                {


                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Calculations finished. Committing move";
                    WriteLogging(LoggerMessage);
                    Board.PawnsInBase(Color)[0].Move(diceRoll);
                }
            }
            else
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: Play] Calculations finished. Committing move";
                WriteLogging(LoggerMessage);
                result.pawnToMove?.Move(diceRoll);
            }

        }
        private (Pawn pawnToMove, bool pass, bool takeout, int takeoutCount) CalculatePlay(int dice)
        {
            var Pawns = Board.OutOfBasePawns(Color);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking how many friendly pawns is on board. Result: {Pawns.Count.ToString()}";

            //Declaring deconstructed variable for CheckIfPawnCanBeTakenOut which will be called upon later
            (int Count, bool IsPossible) takeOut;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Count is equal to {Pawns.Count}";
            if (Pawns.Count > 0)
            {

                //Check if I can reach goal
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking to see if I can reach goal with a pawn";
                var checkPawn = CheckIfPawnCanReachGoal(Pawns, dice);
                if (checkPawn != null) return (checkPawn, false, false, 0);
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachGoal] returned a null pawn. Continuing";
                
                
                //Check if I can reach safezone
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking to see if I can reach Safezone with a pawn";
                checkPawn = CheckIfPawnCanReachSafezone(Pawns, dice);
                if (checkPawn != null) return (checkPawn, false, false, 0);
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachSafezone] returned a null pawn. Continuing";


                    //Check for possible eradication
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking if a pawn has the possibility to eradicate another pawn";
                var pawnToEradicateWith = CheckForPossibleEradication(dice).PawnToEradicateWith;
                if (pawnToEradicateWith != null)
                    return (pawnToEradicateWith, false, false, 0); //Returning eradication move. Method will break here
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] CheckForPossibleEradication returned a null pawn. Continuing";
                

                //Check if pawn(s) can be taken out
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking if pawn can be taken out";
                takeOut = CheckIfPawnCanBeTakenOut(dice);
                if (takeOut.IsPossible)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Can take out {takeOut.Count} pawns. Returning move";
                    return (null, false, true, takeOut.Count); //Taking out pieces. Method will break here
                }


                //No pawns could be taken out
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] CheckIfPawnCanBeTakenOut returned false. Moving piece instead";
                return (ReturnWhatPieceToMove(Pawns, dice), false, false, 0); //Returning piece from Method which calculates best piece to move.
            }



            //Check if pawn(s) can be taken out
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Checking if pawn can be taken out";
            takeOut = CheckIfPawnCanBeTakenOut(dice);
            if (takeOut.IsPossible)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] Can take out {takeOut.Count} pawns. Returning move";
                return (null, false, true, takeOut.Count); //Taking out pieces. Method will break here
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CalculatePlay] CheckIfPawnCanBeTakenOut returned false. Cannot play this turn";
            return (null, true, false, 0); //Pass turn

        }



        #region "Return" Methods
        private Pawn ReturnWhatPieceToMove(List<Pawn> piecesOut, int dice)
        {
            //Making sure piecesOut is not null
            if (piecesOut == null) throw new ArgumentNullException(nameof(piecesOut));


            //Check if pawn has a possibility to end up in goal
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Checking if a pawn has the possibility to end up in goal";
            var checkPawn = CheckIfPawnCanReachGoal(piecesOut, dice);
            if (checkPawn != null) return checkPawn;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] CheckIfPawnCanReachGoal returned null. Continuing";


            //Check if pawn has a possibility to end up in Safezone
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Checking if a pawn has the possibility to end up in safezone";
            checkPawn = CheckIfPawnCanReachSafezone(piecesOut, dice);
            if (checkPawn != null) return checkPawn;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] CheckIfPawnCanReachSafezone returned null. Continuing";


            //Check if a pawn in spawn is blocking
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Checking if spawn point is blocked";
            checkPawn = CheckIfPawnIsBlockingSpawn();
            if (checkPawn != null) return checkPawn;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] CheckIfPawnIsBlockingSpawn returned null. Continuing";


            //Check if any pawn has a possibility to end up in enemy spawn
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Checking if a pawn has the possibility to end up in enemy start square";
            var (pawnEndUpInEnemySpawn, pawnsNotToMove) = CheckIfPawnWillEndUpInEnemySpawn(dice);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Result: {pawnEndUpInEnemySpawn}";


            //Check distance between pawns
            checkPawn = CheckIfPawnDistanceIsTooGreat();
            if (checkPawn != null && !pawnsNotToMove.Contains(checkPawn)) return checkPawn;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] CheckIfPawnDistanceIsTooGreat returned null or the farthest pawn will end up in enemy start square.\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnWhatPieceToMove] Will move the farthest pawn.";
            return ReturnFarthestPawn();
        }
        private Pawn ReturnFarthestPawn()
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: ReturnFarthestPawn] Calculating farthest pawn";
            Pawn pawn = null;
            foreach (var square in Board.TeamPath(Color).Where(square => square.Pawns.Count > 0 &&
                                                                         square.Pawns[0].Color == Color &&
                                                                         square.GetType() != typeof(GoalSquare))) { pawn = square.Pawns[0]; }
            return pawn;
        }
        #endregion

        #region Checking methods
        private (bool result, List<Pawn> pawnsToNotMove) CheckIfPawnWillEndUpInEnemySpawn(int dice)
        {
            var Pawns = Board.OutOfBasePawns(Color);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnWillEndUpInEnemySpawn] Doing calculations to see what friendly pawns can end up in enemy spawn square";
            var Result = false;
            var PawnsNotToMove = new List<Pawn>();
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
            return (Result, PawnsNotToMove);
        }
        private (int Count, bool IsPossible) CheckIfPawnCanBeTakenOut(int dice)
        {
            var Pawns = Board.OutOfBasePawns(Color);
            if (dice == 6)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Dice resulted in a 6\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Checking how many friendly pawns is on board. Result: {Pawns.Count}";
                if (Pawns.Count < 3)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Count is less than three. Will attempt to return double pawns";
                    if (CheckIfPawnIsBlockingSpawn() == null) return (2, true);
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Spawn is blocked. Continuing.";
                }
                if (Pawns.Count == 3)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Count is equal to three. Will attempt to return single takeout";
                    if (CheckIfPawnIsBlockingSpawn() == null) return (1, true);
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Spawn is blocked. Continuing.";

                }
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] All pawns is on board. Will return zero";
                return (0, false);
            }
            if (dice == 1)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Dice resulted in a 1\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Checking how many friendly pawns is on board. Result: {Pawns.Count}";
                if (Pawns.Count < 4)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Count is less than four. Will attempt to return single takeout";
                    if (CheckIfPawnIsBlockingSpawn() == null) return (1, true);
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Spawn is blocked. Continuing.";
                }
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] All pawns is on board. Will return zero";
                return (0, false);
            }
            LoggerMessage +=
                    $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Dice resulted in a 1\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanBeTakenOut] Not eligible to pull out pawn. Will return zero.";
            return (0, false);
        }
        private Pawn CheckIfPawnCanReachSafezone(IEnumerable<Pawn> piecesOut, int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachSafezone] Looping through each friendly pawn on board";
            foreach (var piece in piecesOut)
            {
                var squarePosition = Board.BoardSquares.Find(square => square == piece.CurrentSquare());
                var squarePositionCalc = squarePosition;
                for (var i = 0; i <= dice; i++)
                {
                    squarePositionCalc = Board.GetNext(Board.BoardSquares, squarePositionCalc, Color);
                    if (squarePositionCalc.GetType() != typeof(SafezoneSquare)) continue;
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachSafezone] Can reach a Safezone-square. Returning move";
                    if (squarePosition != null) return squarePosition.Pawns.Find(pawn => pawn.Color == Color);
                }
            }
            return null;
        }
        private Pawn CheckIfPawnCanReachGoal(IEnumerable<Pawn> piecesOut, int dice)
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachGoal] Looping through each friendly pawn on board";
            foreach (var piece in piecesOut)
            {
                var squarePosition = Board.BoardSquares.Find(square => square == piece.CurrentSquare());
                var squarePositionCalc = squarePosition;
                for (var i = 0; i <= dice; i++)
                {
                    squarePositionCalc = Board.GetNext(Board.BoardSquares, squarePositionCalc, Color);
                    if (squarePositionCalc.GetType() != typeof(GoalSquare)) continue;
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnCanReachGoal] Can reach a Goal-square. Returning move";
                    if (squarePosition != null) return squarePosition.Pawns.Find(pawn => pawn.Color == Color);
                }
            }
            return null;
        }
        private Pawn CheckIfPawnIsBlockingSpawn()
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnIsBlockingSpawn] Checking if there is pawns in base";
            if (Board.BaseSquare(Color).Pawns.Count <= 0)
            {
                LoggerMessage +=
                    $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnIsBlockingSpawn] No pawns where found in base, returning null";
                return null;
            }

            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnIsBlockingSpawn] Checking if a friendly pawn is blocking Spawn-point";
            var pawnInTakeOut = Board.StartSquare(Color).Pawns.Find(pawn => pawn.Color == Color);
            return pawnInTakeOut;
        }
        private Pawn CheckIfPawnDistanceIsTooGreat()
        {
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnDistanceIsTooGreat] Calculating distance between min and max pawn";
            var distance = 0;
            Pawn closestPawn = null;
            var farthestPawn = ReturnFarthestPawn();
            var squarePosition = Board.StartSquare(Color); //Start at spawn
            var furthestIndex = 0;
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
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnDistanceIsTooGreat] Distance is: {distance.ToString()} squares";
            if (distance < 10) return null;
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckIfPawnDistanceIsTooGreat] Result is 10 or above. Returning pawn";
            return closestPawn;
        }
        private (bool CanEradicate, Pawn PawnToEradicateWith) CheckForPossibleEradication(int dice)
        {
            var Pawns = Board.OutOfBasePawns(Color);
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Calculating possible eradication";
            var eradication = false;
            Pawn eradicationPawn = null;
            foreach (var pawn in Pawns)
            {
                var squarePosition = pawn.CurrentSquare(); //Start at spawn
                for (var i = 0; i <= dice - 1; i++)
                {
                    squarePosition = Board.GetNext(Board.BoardSquares, squarePosition, Color);
                }

                if (squarePosition.Pawns.Find(p => p.Color != Color) == null) continue;
                eradication = true;
                eradicationPawn = pawn;
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Eradication status: {eradication}";
            if (eradication)
            {
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Can eradicate. Checking if eradication will result in enemy spawn position";
                var pawnsToNotMove = CheckIfPawnWillEndUpInEnemySpawn(dice).pawnsToNotMove;
                if (!pawnsToNotMove.Contains(eradicationPawn) && eradicationPawn != null)
                {
                    LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Eradication will not result in enemy spawn position. Returning move";
                    return (eradication, eradicationPawn);
                }
                LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] Eradication will result in enemy spawn position. Returning Pawn as null";
                return (true, null);
            }
            LoggerMessage += $"\n{DateTime.Now.ToShortTimeString()}: [Method: CheckForPossibleEradication] No eradication possible. Returning Pawn as null";
            return (true, null);
        }
        #endregion


    }
}