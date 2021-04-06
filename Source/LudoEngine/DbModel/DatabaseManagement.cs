using System.IO;
using Microsoft.EntityFrameworkCore;
using LudoEngine.Models;
using LudoEngine.Enum;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace LudoEngine.DbModel
{
    public static class DatabaseManagement
    {
        public static string ConnectionString { get; private set; }

        public static void ReadConnectionString(string filepath)
        {
            ConnectionString = File.ReadAllText(filepath);
        }

        public static void SavePlayer(string Name, int gameId)
        {
            using var db = new LudoContext();

            var player = new Player { PlayerName = Name};
            db.Players.Add(player);

            db.SaveChanges();


            var playerGame = new PlayerGame { GameId = gameId, PlayerId = player.Id};
            db.GamePlayers.Add(playerGame);

            db.SaveChanges();


        }

        public static void SavePawn(TeamColor color, int xPosition , int  yPosition, Game gameID)
        {
            using var db = new LudoContext();

            var pawn = new Pawn { Color = color, XPosition = xPosition, YPosition = yPosition, Game = gameID};

            db.Update(pawn);
            db.SaveChanges();
        }

        public static void SaveGame(string currentTurn, int firstplace, int secondPlace, int thirdPlace, int fourthPlace)
        {
            using var db = new LudoContext();

            var game = new Game { CurrentTurn = currentTurn, FirstPlace = firstplace, SecondPlace = secondPlace, ThirdPlace = thirdPlace, FourthPlace = fourthPlace };

            db.Add(game);
            db.SaveChanges();
        }

        public static void GetGame(int id)
        {
            using var db = new LudoContext();

            var game = db.Games
                .Select(x => x)
                .Where(x => x.Id == id);

            foreach (var item in game)
            {
                Console.WriteLine($"ID: {item.Id}, FirstPlace: {item.FirstPlace}, CurrentTurn: {item.CurrentTurn}");
            }
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

        public static void GetPawnsInGame(Game gameId)
        {
            using var db = new LudoContext();

            var pawns = db.Pawns
                .Where(x => x.Game == gameId);

            foreach (var pawn in pawns)
            {
                Console.WriteLine($"Color: {pawn.Color} Xpos: {pawn.XPosition} Ypos: {pawn.YPosition}");
            }
        }

        public static List<Game> GetGameId(int playerId)
        {
            using var db = new LudoContext();

            var gameId = db.GamePlayers
                .Where(x => x.PlayerId == playerId)
                .Select(x => x.GameId).ToList();

            return ( from item in gameId
                     from game in db.Games
                     where game.Id == item
                     select game
                     ).ToList();
        }
    }
}