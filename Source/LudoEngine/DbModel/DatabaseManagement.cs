using System.IO;
using Microsoft.EntityFrameworkCore;
using LudoEngine.Models;
using LudoEngine.Enum;
using System.Linq;
using System;
using System.Collections.Generic;
using LudoEngine.BoardUnits.Main;
using System.Threading;
using LudoConsole.Main;

namespace LudoEngine.DbModel
{
    public static class DatabaseManagement
    {
        public static string ConnectionString { get; private set; }
        public static Thread SaveThread;
        private static GamePlay _gamePlay { get; set; }
        public static void SaveInit(GamePlay gamePlay)
        {
            
            _gamePlay = gamePlay;
            SaveThread = new Thread(new ThreadStart( () => save()));
            SaveThread.IsBackground = true;
        }
        public static void Save()
        {
            if (!SaveThread.IsAlive)
            {
                SaveThread = new Thread(new ThreadStart(() => save()));
                SaveThread.Start();
            }
        }

        public static void ReadConnectionString(string filepath)
        {
            ConnectionString = File.ReadAllText(filepath);
        }

        public static void SavePlayer(string Name, int gameId)
        {
            using var db = new LudoContext();

            var player = new Player { PlayerName = Name };
            db.Players.Add(player);

            db.SaveChanges();


            var playerGame = new PlayerGame { GameId = gameId, PlayerId = player.Id };
            db.GamePlayers.Add(playerGame);

            db.SaveChanges();
        }

        public static void SaveAndGetGame(TeamColor currentTurn)
        {
            using var db = new LudoContext();

            var saveGame = new Game { CurrentTurn = currentTurn};
            db.Games.Update(saveGame);
            db.SaveChanges();

            var getGame = db.Games
                .Where(x => x.Id ==  saveGame.Id)
                .First();

            StageSaving.Game = getGame;
        }

        public static List<Game> GetGames()
        {
            using var db = new LudoContext();

            var game = db.Games
                .Select(x => x)
                .OrderByDescending(x => x.LastSaved);

            List<Game> games = new();
            if (game != null)
            {
                games.AddRange(game);
            }

            return games;
        }

        public static void GetPlayersInGame(int gameId)
        {
            using var db = new LudoContext();


            var player = db.Players;
            var gamePlayers = db.GamePlayers;

            var players = player
                .Join(
                gamePlayers,
                player => player.Id,
                gamePlayers => gamePlayers.PlayerId,
                (player, gamePlayers) => new
                {
                    player.Id,
                    player.PlayerName,
                    gamePlayers.GameId
                })
                .Where(x => x.GameId == gameId);

            foreach (var item in players)
            {
                Console.WriteLine(item.PlayerName);
            }
        }

        public static List<PawnSavePoint> GetPawnPositionsInGame(Game gameId)
        {
            using var db = new LudoContext();

            var savePoints = db.PawnSavePoints
                .Where(x => x.GameId == gameId.Id);

            List<PawnSavePoint> savepointList = new();

            foreach (var item in savePoints)
            {
                PawnSavePoint savepoint = new PawnSavePoint()
                { 
                    Id = item.Id,
                    GameId = item.GameId,
                    Color = item.Color,
                    XPosition = item.XPosition,
                    YPosition = item.YPosition
                };
                savepointList.Add(savepoint);
            }

            return savepointList;
        }

        public static List<Game> GetGameId(int playerId)
        {
            using var db = new LudoContext();

            var gameId = db.GamePlayers
                .Where(x => x.PlayerId == playerId)
                .Select(x => x.GameId).ToList();

            return (from item in gameId
                    from game in db.Games
                    where game.Id == item
                    select game
                     ).ToList();
        }

        private static void save()
        {
            
            TeamColor currentTeam = _gamePlay.CurrentPlayer(stageSaving: true).Color;
            StageSaving.Pawns = Board.GetTeamPawns(currentTeam);

            List<Pawn> pawns = StageSaving.Pawns;
            Game game = new();
            if (StageSaving.Game != null)
            {
                game = StageSaving.Game;
            }
            else
            {
                SaveAndGetGame(currentTeam);
                game = StageSaving.Game;
            }
            using var db = new LudoContext();

            
            if (db.PawnSavePoints.Any(x => x.Color == currentTeam && x.GameId == game.Id))
            {
                var querry = db.PawnSavePoints
                .Where(x => x.Color == currentTeam && x.GameId == game.Id);

                var pawnsArray = pawns.ToArray();
                foreach (var dbData in querry)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        dbData.Color = pawnsArray[i].Color;
                        dbData.XPosition = pawnsArray[i].CurrentSquare().BoardX;
                        dbData.YPosition = pawnsArray[i].CurrentSquare().BoardY;
                        dbData.GameId = game.Id;

                        db.PawnSavePoints.Attach(dbData);
                        db.Entry(dbData).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                foreach (var pawn in pawns)
                {
                    var pawnPosition = new PawnSavePoint { Color = pawn.Color, XPosition = pawn.CurrentSquare().BoardX, YPosition = pawn.CurrentSquare().BoardY, GameId = game.Id };

                    db.Update(pawnPosition);
                }
            }

            db.SaveChanges();

            //foreach (var item in pawns)
            //{
                //var querry = db.PawnSavePoints
                //    .Where(x => x.GameId == game.Id && x.PawnId == item.Id && x.Color == item.Color)
                //    .FirstOrDefault();
                //if (querry != null)
                //{
                //    querry.XPosition = item.CurrentSquare().BoardX;
                //    querry.YPosition = item.CurrentSquare().BoardY;
                //}
                //else
                //{
                //    var pawnPosition = new PawnSavePoint { PawnId = item.Id, Color = item.Color, XPosition = item.CurrentSquare().BoardX, YPosition = item.CurrentSquare().BoardY, GameId = game.Id };
                //    db.Add(pawnPosition);
                //}
            //}

            var result = db.Games
                .Where(x => x.Id == game.Id)
                .SingleOrDefault();
            if (result != null)
            {
                result.CurrentTurn = _gamePlay.NextPlayerForSave();
                result.LastSaved = DateTime.Now;
                db.Games.Attach(result);
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


    }

    public static class StageSaving {
        public static List<Pawn> Pawns { get; set;}

        public static Game Game { get; set; }

        public static List<Player> Players { get; set; }

        public static List<PawnSavePoint> TeamPosition { get; set; }

        public static int CurrentTeam { get; set; }
    }

}