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

            var game = db.Games.Where(x => x.Id == gameId);
            var player = new Player { PlayerName = Name};
            db.Players.Add(player);

            var playerGame = new PlayerGame { GameId = gameId, PlayerId = player.Id};

            db.SaveChanges();


        }

        public static void SavePawn(TeamColor color, int xPosition , int  yPosition, int gameID)
        {
            using var db = new LudoContext();

            var pawn = new Pawn { Color = color, XPosition = xPosition, YPosition = yPosition, GameID = gameID};

            db.Add(pawn);
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
                    Id = player.Id,
                    PlayerName = player.PlayerName,
                    GameId = gamePlayers.GameId
                })
                .Where(x => x.GameId == gameId);

            foreach (var item in players)
            {
                Console.WriteLine(item.PlayerName);
            }
                

            
        }
    }
}