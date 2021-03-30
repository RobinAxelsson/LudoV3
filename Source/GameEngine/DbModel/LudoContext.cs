using System.Collections;
using System.Collections.Generic;
using GameEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace GameEngine.DbModel
{
    public class StarWarsContext : DbContext
    {
        public string ConnectionString;
        public DbSet<Player> Players;
        public DbSet<GameState> GameStates;
        public DbSet<GameResult> GameResults;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt");
            optionsbuilder.UseSqlServer(DatabaseManagement.ConnectionString);
        }
    }
}