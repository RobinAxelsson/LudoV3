using LudoEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoEngine.DbModel
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<Game> GameResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            DatabaseManagement.ReadConnectionString(@"DbModel/connection.txt");
            optionsbuilder.UseSqlServer(DatabaseManagement.ConnectionString);
        }
    }
}