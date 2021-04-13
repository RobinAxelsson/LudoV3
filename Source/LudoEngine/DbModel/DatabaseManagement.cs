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
            SaveThread = new Thread(new ThreadStart(() => save(gamePlay)));
            SaveThread.IsBackground = true;
            GamePlay.OnPlayerEndsRoundEvent += Save;
        }
        public static void Save()
        {
            if (!SaveThread.IsAlive)
            {
                SaveThread = new Thread(new ThreadStart(() => save(_gamePlay)));
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

        public static void SavePawn(TeamColor color, int xPosition, int yPosition, Game gameID)
        {
            using var db = new LudoContext();

            var pawn = new PawnSavePoint { Color = color, XPosition = xPosition, YPosition = yPosition, GameId = gameID.Id };

            db.Update(pawn);
            db.SaveChanges();
        }

        public static void SaveGame(TeamColor currentTurn, int firstplace, int secondPlace, int thirdPlace, int fourthPlace)
        {
            using var db = new LudoContext();

            var game = new Game { CurrentTurn = currentTurn, FirstPlace = firstplace, SecondPlace = secondPlace, ThirdPlace = thirdPlace, FourthPlace = fourthPlace };

            db.Add(game);
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

        public static List<(TeamColor color, (int X, int Y) position)> GetPawnPositionsInGame(Game gameId)
        {
            using var db = new LudoContext();

            var savePoints = db.PawnSavePoints
                .Where(x => x.GameId == gameId.Id);

            List<(TeamColor color, (int X, int Y) position)> savepointList = new();

            foreach (var item in savePoints)
            {
                savepointList.Add((item.Color, (item.XPosition, item.YPosition)));
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

        private static void save(GamePlay gamePlay)
        {
            TeamColor currentTeam = gamePlay.CurrentPlayer().Color;
            StageSaving.Pawns = Board.GetTeamPawns(currentTeam);

            List<Pawn> pawns = StageSaving.Pawns;
            Game game = StageSaving.Game;
            using var db = new LudoContext();

            foreach (var item in pawns)
            {
                var querry = db.PawnSavePoints
                    .Where(x => x.GameId == game.Id && x.PawnId == item.Id && x.Color == item.Color)
                    .FirstOrDefault();
                if (querry != null)
                {
                    querry.XPosition = item.CurrentSquare().BoardX;
                    querry.YPosition = item.CurrentSquare().BoardY;
                }
                else
                {
                    var pawnPosition = new PawnSavePoint { PawnId = item.Id, Color = item.Color, XPosition = item.CurrentSquare().BoardX, YPosition = item.CurrentSquare().BoardY, GameId = game.Id };
                    db.Add(pawnPosition);
                }
            db.SaveChanges();
            }

            var result = db.Games
                .Where(x => x.Id == game.Id)
                .SingleOrDefault();
            if (result != null)
            {
                result.CurrentTurn = gamePlay.CachedPlayer();
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

        public static List<(TeamColor color, (int X, int Y) position)> TeamPosition { get; set; }
    }

}